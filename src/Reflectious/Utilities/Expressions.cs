using System;
using System.Linq.Expressions;

namespace Reflectious
{
    /// <summary>
    /// Some default expressions.
    /// </summary>
    public static class Expressions
    {
        public static Expression<Func<T, T>> Identity<T>()
        {
            return t => t;
        }
        
        public static Expression<Func<T, bool>> True<T>()
        {
            return t => true;
        }
        
        public static Expression<Func<T, bool>> False<T>()
        {
            return t => false;
        }
        
        public static Expression<Func<T, T>> Default<T>()
        {
            return t => default(T);
        }
    }
}