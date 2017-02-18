using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;

namespace RuleEngine.Builders.StringBuilders
{
	public class ConcatBuilder : ExpressionBuilder
	{
		private readonly MethodInfo _method;
		private readonly ExpressionFactory _factory;

		public ConcatBuilder(ExpressionFactory factory)
		{
			var stringType = typeof(string);
			_method = stringType.GetMethods().Where(c => c.Name == "Concat").First(c => c.GetParameters().Length == 2);
			_factory = factory;
		}

		public override Expression BuildExpression(Locator locator, Expression parent, int level)
		{
			var resultLeft = locator.Left == null
				? Expression.Constant("")
				: _factory.BuildLocatorExpression(locator.Left, parent, level);

			var resultRight = locator.Right == null
				? Expression.Constant("")
				: _factory.BuildLocatorExpression(locator.Right, parent, level);

			return Expression.Call(_method, resultLeft, resultRight);
		}
	}
}