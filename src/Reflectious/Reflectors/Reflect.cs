using System;
using System.Linq.Expressions;

namespace Reflectious
{
    public static class Reflect
    {
        public static StaticReflector<T> Type<T>() 
            where T : class
        {
            return new StaticReflector<T>();
        }
        
        public static StaticReflector Type(Type type)
        {
            return new StaticReflector(type);
        }
        
        public static InstanceReflector<T> Instance<T>(T instance)
        {
            return new InstanceReflector<T>(new StrongInstanceGetter<T>(instance));
        }
        
        public static LambdaExpressionReflector Expression(LambdaExpression expression)
        {
            return new LambdaExpressionReflector(expression);
        }
        
        public static LambdaExpressionReflector<TObj, TReturn> Expression<TObj, TReturn>(Expression<Func<TObj, TReturn>> expression)
        {
            return new LambdaExpressionReflector<TObj, TReturn>(expression);
        }
    }
}