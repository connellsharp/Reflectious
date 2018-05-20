using System;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace Reflectious
{
    /// <summary>
    /// Exposes fluent API for a lambda expression.
    /// </summary>
    /// <remarks>
    /// From http://stackoverflow.com/a/672212 and http://stackoverflow.com/a/17116267
    /// </remarks>
    public class LambdaExpressionReflector<TObj, TReturn> : LambdaExpressionReflector
    {
        private readonly Expression<Func<TObj, TReturn>> _expression;

        public LambdaExpressionReflector([NotNull] Expression<Func<TObj, TReturn>> expression)
            : base(expression)
        {
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public LambdaExpressionReflector<TObj, TChained> Chain<TChained>(Expression<Func<TReturn, TChained>> nextExpression)
        {
            var chainedExpr = _expression.Chain(nextExpression);
            return new LambdaExpressionReflector<TObj, TChained>(chainedExpr);
        }

        public IMethod GetMethod()
        {
            return new ReflectionMethod(GetMethodInfo());
        }

        public IProperty GetProperty()
        {
            return new ReflectionProperty(GetPropertyInfo());
        }

        public MethodInfo GetMethodInfo()
        {
            var methodCallExpr = _expression.Body as MethodCallExpression;
            if (methodCallExpr == null)
                throw new ArgumentException(string.Format("Expression '{0}' does not refer to a method.", _expression));

            return methodCallExpr.Method;
        }

        public MemberInfo GetMemberInfo()
        {
            var memberExpr = GetMemberExpression();
            if (memberExpr == null)
                throw new ArgumentException(string.Format("Expression '{0}' does not refer to a member expression.", _expression));

            return memberExpr.Member;
        }

        public PropertyInfo GetPropertyInfo()
        {
            var propInfo = GetMemberInfo() as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", _expression));

            return propInfo;
        }

        public FieldInfo GetFieldInfo()
        {
            var fieldInfo = GetMemberInfo() as FieldInfo;
            if (fieldInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property, not a field.", _expression));

            return fieldInfo;
        }

        private MemberExpression GetMemberExpression()
        {   
            // sometimes the expression comes in as Convert(originalexpression)
            var unExp = _expression.Body as UnaryExpression;
            if (unExp != null)
            {
                var operand = unExp.Operand as MemberExpression;
                if (operand != null)
                    return operand;

                throw new ArgumentException();
            }

            var expr = _expression.Body as MemberExpression;
            if (expr != null)
                return expr;

            throw new ArgumentException();
        }
    }

    public class LambdaExpressionReflector
    {
        private readonly LambdaExpression _expression;

        public LambdaExpressionReflector(LambdaExpression expression)
        {
            _expression = expression;
        }

        public LambdaExpressionReflector Chain(LambdaExpression nextExpression)
        {
            var chainedExpr = _expression.Chain(nextExpression);
            return new LambdaExpressionReflector(chainedExpr);
        }
    }
}