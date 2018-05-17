using System.Linq.Expressions;

namespace Reflectious
{
    public class ExpressionReflector
    {
        private readonly Expression _expression;

        public ExpressionReflector(Expression expression)
        {
            _expression = expression;
        }
    }
}