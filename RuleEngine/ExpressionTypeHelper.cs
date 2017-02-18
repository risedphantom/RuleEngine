using System;
using System.Linq.Expressions;
using RuleEngine.Model;

namespace RuleEngine
{
    public static class ExpressionTypeHelper
    {
        /// <exception cref="ArgumentOutOfRangeException">Condition.</exception>
        public static ExpressionType GetByAggregator(AggregatorType aggregator)
        {
            switch (aggregator)
            {
                case AggregatorType.And:
                    return ExpressionType.AndAlso;
                case AggregatorType.Or:
                    return ExpressionType.OrElse;
                case AggregatorType.Xor:
                    return ExpressionType.ExclusiveOr;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <exception cref="OverflowException"><paramref name="operand" /> Enum Names should be equivalent</exception>
        /// <exception cref="ArgumentNullException"><paramref name="operand" /> or <paramref name="operand" /> is null. </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="operand" /> is not an <see cref="ExpressionType" />.-or- <paramref name="operand" />
        ///     <para />
        ///     is either an empty string or only contains white space.-or- <paramref name="operand" /> is a name, but not one of
        ///     the named constants defined for the enumeration.
        /// </exception>
        public static ExpressionType GetByConditionOperand(ConditionOperand operand)
        {
            var result = (ExpressionType) Enum.Parse(typeof(ExpressionType), operand.ToString());
            return result;
        }

        public static ExpressionType GetByLocator(LocatorOperation locatorOperation)
        {
            switch (locatorOperation)
            {
                case LocatorOperation.None:
                case LocatorOperation.Equal:
                    return ExpressionType.Equal;
                case LocatorOperation.NotEqual:
                    return ExpressionType.NotEqual;
                default:
                    throw new ArgumentOutOfRangeException(nameof(locatorOperation), locatorOperation, null);
            }
        }
    }
}