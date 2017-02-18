using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;
using RuleEngine.Model.Locators.StringLocators;

namespace RuleEngine.Builders.StringBuilders
{
    public class SubstringBuilder : ExpressionBuilder
    {
        private readonly MethodInfo _method;

        public SubstringBuilder()
        {
			_method = typeof(NonStrictStringExtensions).GetMethod("Substring");
		}

        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var substringLocator = (SubstringLocator) locator;

            return Expression.Call(_method, parent, Expression.Constant(substringLocator.Offset), Expression.Constant(substringLocator.Length));
        }
    }
}