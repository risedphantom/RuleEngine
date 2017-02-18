using System.Linq.Expressions;
using RuleEngine.Model;

namespace RuleEngine
{
    public abstract class ExpressionBuilder
    {
        public abstract Expression BuildExpression(Locator locator, Expression parent, int level);
    }
}