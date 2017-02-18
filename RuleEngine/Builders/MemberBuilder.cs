using System.Linq.Expressions;
using RuleEngine.Model;
using RuleEngine.Model.Locators;

namespace RuleEngine.Builders
{
	public class MemberBuilder : ExpressionBuilder
	{
		private readonly ExpressionFactory _factory;

		public MemberBuilder(ExpressionFactory factory)
		{
			_factory = factory;
		}

		public override Expression BuildExpression(Locator locator, Expression parent, int level)
		{
			var innerLocator = (MemberLocator)locator;
			Expression result = Expression.Property(parent, innerLocator.Property);
			if (innerLocator.AsString)
				result = Expression.Call(result, innerLocator.GetType().GetMethod("ToString"));

			if (locator.Left == null)
				return result;

			result = _factory.BuildLocatorExpression(locator.Left, result, level);
			if (locator.Right == null)
				return result;

			var right = _factory.BuildLocatorExpression(locator.Right, result, level);
			var binaryOperator = ExpressionTypeHelper.GetByLocator(innerLocator.Operation);
			return Expression.MakeBinary(binaryOperator, result, right);
		}
	}
}