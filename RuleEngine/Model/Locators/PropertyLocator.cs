namespace RuleEngine.Model.Locators
{
    public sealed class PropertyLocator : Locator
    {
        public LocatorOperation Operation = LocatorOperation.Equal;

        public PropertyLocator()
        {
        }

        public PropertyLocator(string property, LocatorOperation op = LocatorOperation.None) : this()
        {
            Property = property;
            Operation = op;
        }

        public string Property { get; set; }

        public override LocatorType Type => LocatorType.Property;
    }
}