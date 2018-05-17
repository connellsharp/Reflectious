using System;
using System.Linq.Expressions;

namespace Reflectious
{
    public static class ReflectorExtensions
    {   
        public static StaticReflector Reflect(this Type type)
        {
            return new StaticReflector(type);
        }
        
        public static InstanceReflector<T> Reflect<T>(this T instance)
        {
            return new InstanceReflector<T>(new StrongInstanceGetter<T>(instance));
        }
        
        public static LambdaExpressionReflector Reflect(this LambdaExpression expression)
        {
            return new LambdaExpressionReflector(expression);
        }
        
        public static LambdaExpressionReflector<TObj, TReturn> Reflect<TObj, TReturn>(this Expression<Func<TObj, TReturn>> expression)
        {
            return new LambdaExpressionReflector<TObj, TReturn>(expression);
        }
    }
}