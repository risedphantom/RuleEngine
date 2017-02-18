using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;
using RuleEngine.Model.Locators;

namespace RuleEngine.Builders
{
    public class PredicateBuilder : ExpressionBuilder
    {
        private readonly ExpressionFactory _factory;

        private readonly MethodInfo _predicateMethod =
            typeof(LinqExtension).GetMethods()
                .First(m => m.Name == "Predicate");

        public PredicateBuilder(ExpressionFactory factory)
        {
            _factory = factory;
        }

        /// <exception cref="OverflowException">Enum Names should be equivalent</exception>
        /// <exception cref="InvalidOperationException">!predicateLocator.Conditions.Any().</exception>
        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var predicateLocator = (PredicateLocator) locator;

            if (predicateLocator.Conditions.Any())
            {
                var type = parent.Type;
                var parameter = Expression.Parameter(type, $"{type.Name.ToLower()}{level}");
                var lambdaBody = _factory.BuildExpressionForConditions(predicateLocator.Conditions, parameter,
                    level);
                Expression lambda = Expression.Lambda(lambdaBody, parameter);
                parent = MakePredicateExpression(lambda, parent);
                return parent;
            }
            throw new InvalidOperationException();
        }


        public Expression MakePredicateExpression(Expression lambda, Expression prevCall)
        {
            var method = _predicateMethod.MakeGenericMethod(prevCall.Type);
            var predicateExpression = Expression.Call(null, method, prevCall, lambda);
            return predicateExpression;
        }
    }
}