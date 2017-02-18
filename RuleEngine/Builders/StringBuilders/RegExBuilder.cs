using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using RuleEngine.Model;
using RuleEngine.Model.Locators.StringLocators;

namespace RuleEngine.Builders.StringBuilders
{
    public class RegExBuilder : ExpressionBuilder
    {
        private readonly MethodInfo _method;

        public RegExBuilder()
        {
            _method = typeof(Regex).GetMethod("IsMatch",
                new[] { typeof(string), typeof(string), typeof(RegexOptions) });
        }

        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var regExLocator = (RegExLocator) locator;

            return Expression.Call(_method,
                parent,
                Expression.Constant(regExLocator.RegEx),
                Expression.Constant(RegexOptions.IgnoreCase, typeof(RegexOptions))
                );
        }
    }
}