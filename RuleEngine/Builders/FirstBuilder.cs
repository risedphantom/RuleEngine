using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;

namespace RuleEngine.Builders
{
	public class FirstBuilder : ExpressionBuilder
    {
        private readonly ExpressionFactory _factory;

        private readonly MethodInfo _firstLambdaMethod =
            typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "First")
                .Single(m => m.GetParameters().Length == 2);

        private readonly MethodInfo _firstMethod =
            typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "First")
                .Single(m => m.GetParameters().Length == 1);

        public FirstBuilder(ExpressionFactory factory)
        {
            _factory = factory;
        }

        /// <exception cref="OverflowException"><paramref name="locator.Conditions" /> Enum Names should be equivalent</exception>
        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var result = parent; //ExpressionHelper.MakeAsEnumerableExpression(parent);
            if (locator.Conditions != null && locator.Conditions.Any())
            {
                var type = result.Type.GenericTypeArguments.First();
                var parameter = Expression.Parameter(type, $"{type.Name.ToLower()}{level}");
                var lambdaBody = _factory.BuildExpressionForConditions(locator.Conditions, parameter, level);
                Expression lambda = Expression.Lambda(lambdaBody, parameter);
                result = MakeFirstExpression(lambda, result);
                return result;
            }
            result = MakeFirstExpression(result);
            return result;
        }

        public Expression MakeFirstExpression(Expression prevCall)
        {
            var method = _firstMethod.MakeGenericMethod(prevCall.Type.GetGenericArguments());
            var firstExpression = Expression.Call(null, method, prevCall);
            return firstExpression;
        }

        public Expression MakeFirstExpression(Expression lambda, Expression prevCall)
        {
            var method = _firstLambdaMethod.MakeGenericMethod(prevCall.Type.GetGenericArguments());
            var firstExpression = Expression.Call(null, method, prevCall, lambda);
            return firstExpression;
        }
    }
}