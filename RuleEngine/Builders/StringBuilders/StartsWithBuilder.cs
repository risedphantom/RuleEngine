using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;
using RuleEngine.Model.Locators.StringLocators;

namespace RuleEngine.Builders.StringBuilders
{
    public class StartsWithBuilder : ExpressionBuilder
    {
        private readonly MethodInfo _method;

        public StartsWithBuilder()
        {
			_method = typeof(NonStrictStringExtensions).GetMethod("StartsWith");
		}

        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
			var substringLocator = (StartsWithLocator)locator;
			return Expression.Call(_method, parent, Expression.Constant(substringLocator.StartWith));
		}
    }
}