using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;

namespace RuleEngine.Builders.StringBuilders
{
	public class ReverseBuilder : ExpressionBuilder
	{
		private readonly MethodInfo _method;
		private readonly ExpressionFactory _factory;

		public ReverseBuilder(ExpressionFactory factory)
		{
			_method = typeof(NonStrictStringExtensions).GetMethod("Reverse");
			_factory = factory;
		}

		public override Expression BuildExpression(Locator locator, Expression parent, int level)
		{
			var resultLeft = locator.Left == null
				? Expression.Constant("")
				: _factory.BuildLocatorExpression(locator.Left, parent, level);

			return Expression.Call(_method, resultLeft);
		}
	}
}
