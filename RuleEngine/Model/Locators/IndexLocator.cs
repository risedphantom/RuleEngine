namespace RuleEngine.Model.Locators
{
    public class IndexLocator : Locator
    {
        public IndexLocator(string key)
        {
            Value = key;
        }

        public override LocatorType Type => LocatorType.Index;

        public object Value { get; set; }
    }
}