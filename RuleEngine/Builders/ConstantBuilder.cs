using System.Linq.Expressions;
using RuleEngine.Model;
using RuleEngine.Model.Locators;

namespace RuleEngine.Builders
{
	public class ConstantBuilder : ExpressionBuilder
	{
		private readonly ExpressionFactory _factory;

		public ConstantBuilder(ExpressionFactory factory)
		{
			_factory = factory;
		}

		public override Expression BuildExpression(Locator locator, Expression parent, int level)
		{
			var innerLocator = (ConstantLocator)locator;
			Expression result = Expression.Constant(innerLocator.Value);
			if (innerLocator.TryCast)
				result = Expression.Convert(result, parent.Type);

			return locator.Left == null ? result : _factory.BuildLocatorExpression(locator.Left, result, level);
		}
	}
}