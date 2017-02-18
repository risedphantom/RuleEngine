namespace RuleEngine.Model
{
    public enum RuleApplyMode
    {
        Override,
        Merge,
    }

    public class ExtendedRule<T> : Rule where T:class
    {
        public T ExtendedInformation { get; set; }
    }
}