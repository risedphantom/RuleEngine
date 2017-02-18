using System.Linq.Expressions;
using RuleEngine.Model;
using RuleEngine.Model.Locators;

namespace RuleEngine.Builders
{
    public class IndexBuilder : ExpressionBuilder
    {
        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var dictionaryKeyLocator = (IndexLocator) locator;
            var indexProperty = parent.Type.GetProperty("Item");
            var result = Expression.MakeIndex(parent, indexProperty, new Expression[] { Expression.Constant(dictionaryKeyLocator.Value) });
            return result;
        }
    }
}