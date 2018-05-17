using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectious
{
    /// <summary>
    /// Idea and code from https://rogerjohansson.blog/2008/02/28/linq-expressions-creating-objects/
    /// </summary>
    internal class CompiledLambdaConstructor : IMethod
    {
        private readonly ObjectActivator _objActivator;

        public CompiledLambdaConstructor(ConstructorInfo ctorInfo)
        {
            _objActivator = CreateObjectActivator(ctorInfo);
        }

        public object Invoke(object instance, object[] args)
        {
            Debug.Assert(instance == null);
            return _objActivator.Invoke(args);
        }

        public MethodInfo GetMethodInfo()
        {
            throw new NotSupportedException("Cannot get MethodInfo from compiled lambda constructor.");
        }

        private static ObjectActivator CreateObjectActivator(ConstructorInfo ctorInfo)
        {
            ParameterInfo[] paramsInfo = ctorInfo.GetParameters();

            //create a single param of type object[]
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp = new Expression[paramsInfo.Length];

            //pick each arg from the params array and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            // Make a NewExpression that calls the ctor with the args we just created
            NewExpression newExp = Expression.New(ctorInfo, argsExp);

            // Create a lambda with the New expression as body and our param object[] as arg
            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator), newExp, param);

            // Compile it
            ObjectActivator compiled = (ObjectActivator) lambda.Compile();
            return compiled;
        }

        private delegate object ObjectActivator(params object[] args);
    }
}