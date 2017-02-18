namespace RuleEngine.Model
{
    public class Condition
    {
        public Condition()
        {
            Operand = ConditionOperand.Equal;
        }

        public Locator Left { get; set; }

        public Locator Right { get; set; }

        public ConditionOperand Operand { get; set; }

        public AggregatorType Aggregator { get; set; }
    }
}