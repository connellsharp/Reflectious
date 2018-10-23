using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflectious
{
    internal class ExpressionPropertyFinder<TSource, TReturn> : IPropertyFinder
    {
        private readonly PropertyInfo _propertyInfo;

        public ExpressionPropertyFinder(Expression<Func<TSource, TReturn>> propertyExpression)
        {
            _propertyInfo = Reflect.Expression(propertyExpression).GetPropertyInfo();
        }

        public IProperty Find()
        {
            return new ReflectionProperty(_propertyInfo);
        }

        public Type PropertyType
        {
            get { return _propertyInfo.PropertyType; }
        }
    }
}