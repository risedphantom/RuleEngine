using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;

namespace RuleEngine.Builders
{
	public class AnyBuilder : ExpressionBuilder
    {
        private readonly MethodInfo _anyLambdaMethod =
            typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "Any")
                .Single(m => m.GetParameters().Length == 2);

        private readonly MethodInfo _anyMethod =
            typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "Any")
                .Single(m => m.GetParameters().Length == 1);

        private readonly ExpressionFactory _factory;

        public AnyBuilder(ExpressionFactory factory)
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
                result = MakeAnyExpression(lambda, result);
                return result;
            }
            if (locator.Conditions != null && locator.Left != null && !locator.Conditions.Any())
            {
                var type = result.Type.GenericTypeArguments.First();
                var parameter = Expression.Parameter(type, $"{type.Name.ToLower()}{level}");
                var lambdaBody = _factory.BuildLocatorExpression(locator.Left, parameter, level);
                if (locator.Left.Type == LocatorType.Constant)
                {
                    lambdaBody = Expression.Equal(parameter, lambdaBody);
                }
                Expression lambda = Expression.Lambda(lambdaBody, parameter);
                result = MakeAnyExpression(lambda, result);
                return result;
            }
            result = MakeAnyExpression(result);
            return result;
        }

        public Expression MakeAnyExpression(Expression lambda, Expression prevCall)
        {
            var method = _anyLambdaMethod.MakeGenericMethod(prevCall.Type.GetGenericArguments());
            var anyExpression = Expression.Call(null, method, prevCall, lambda);
            return anyExpression;
        }

        public Expression MakeAnyExpression(Expression prevCall)
        {
            var typeArg = prevCall.Type;
            var method = _anyMethod;
            if (prevCall.Type.IsGenericType && prevCall.Type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var keyType = prevCall.Type.GetGenericArguments()[0];
                var valueType = prevCall.Type.GetGenericArguments()[1];
                typeArg = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);
                method = method.MakeGenericMethod(typeArg);
            }
            else
            {
                method = method.MakeGenericMethod(typeArg.GetGenericArguments());
            }

            var anyExpression = Expression.Call(null, method, prevCall);
            return anyExpression;
        }
    }
}