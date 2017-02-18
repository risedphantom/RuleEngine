using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;
using RuleEngine.Model.Locators.StringLocators;

namespace RuleEngine.Builders.StringBuilders
{
    public class EndWithBuilder : ExpressionBuilder
    {
        private readonly MethodInfo _method;

        public EndWithBuilder()
        {
	        _method = typeof(NonStrictStringExtensions).GetMethod("EndsWith");
        }

        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var endwithLocator = (EndWithLocator) locator;
            return Expression.Call(_method, parent, Expression.Constant(endwithLocator.EndWith));
        }
    }
}