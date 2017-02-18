using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RuleEngine.Builders;
using RuleEngine.Builders.StringBuilders;
using RuleEngine.Model;

namespace RuleEngine
{
	public class ExpressionFactory
    {
        private Dictionary<LocatorType, ExpressionBuilder> _availableBuilders;

        public ExpressionFactory()
        {
            Initialize();
        }

        private void Initialize()
        {
            _availableBuilders = new Dictionary<LocatorType, ExpressionBuilder>
            {
                { LocatorType.Property, new MemberBuilder(this) },
                { LocatorType.Any, new AnyBuilder(this) },
                { LocatorType.First, new FirstBuilder(this) },
                { LocatorType.Last, new LastBuilder(this) },
                { LocatorType.Predicate, new PredicateBuilder(this) },
                { LocatorType.Constant, new ConstantBuilder(this) },
                { LocatorType.ElementAt, new ElementAtBuilder() },
                { LocatorType.Substring, new SubstringBuilder() },
                { LocatorType.StartWith, new StartsWithBuilder() },
                { LocatorType.EndWith, new EndWithBuilder() },
                { LocatorType.Concat, new ConcatBuilder(this) },
                { LocatorType.RegEx, new RegExBuilder() },
                { LocatorType.Contains, new ContainsBuilder(this) },
                { LocatorType.Index, new IndexBuilder() },
				{ LocatorType.Reverse, new ReverseBuilder(this) },
				{ LocatorType.Length, new LengthBuilder(this) },
			};
        }

        /// <exception cref="OverflowException"><paramref name="condition.Operand" /> Enum Names should be equivalent</exception>
        private Expression BuildExpressionForCondition(Condition condition, Expression paramUser, int level = 0)
        {
            var leftLocatorExpression = BuildLocatorExpression(condition.Left, paramUser, ++level);
            if (condition.Right != null && condition.Right.IsLambda)
            {
                return BuildLocatorExpression(condition.Right, leftLocatorExpression, ++level);
            }

            if (condition.Right == null)
            {
                return leftLocatorExpression;
            }
            var rightLocatorExpression = BuildLocatorExpression(condition.Right, paramUser, ++level);

            var expressionType = ExpressionTypeHelper.GetByConditionOperand(condition.Operand);
            var result = Expression.MakeBinary(expressionType, leftLocatorExpression,
                Expression.Convert(rightLocatorExpression, leftLocatorExpression.Type));
            return result;
        }


        internal Expression BuildLocatorExpression(Locator locator, Expression parent, int level = 0)
        {
            var builder = _availableBuilders[locator.Type];
            return builder.BuildExpression(locator, parent, ++level);
        }

        /// <exception cref="OverflowException"><paramref name="conditions" /> Enum Names should be equivalent</exception>
        public Expression BuildExpressionForConditions(ICollection<Condition> conditions, ParameterExpression parameter,
            int level)
        {
            Expression result = Expression.Constant(true);
            if (conditions.Any())
            {
                result = BuildExpressionForCondition(conditions.First(), parameter, ++level);
                if (conditions.Count == 1)
                {
                    return result;
                }
            }

            foreach (var condition in conditions.Skip(1))
            {
                var binaryOperation = ExpressionTypeHelper.GetByAggregator(condition.Aggregator);
                var conditionExpression = BuildExpressionForCondition(condition, parameter, ++level);
                result = Expression.MakeBinary(binaryOperation, result, Expression.Convert(conditionExpression, result.Type));
            }
            return result;
        }
    }
}