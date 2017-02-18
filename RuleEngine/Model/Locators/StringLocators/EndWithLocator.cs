namespace RuleEngine.Model.Locators.StringLocators
{
    public class EndWithLocator : Locator
    {
        public EndWithLocator()
        {
        }

        public EndWithLocator(string endWith)
        {
            EndWith = endWith;
        }

        public override LocatorType Type => LocatorType.EndWith;

        public string EndWith { get; set; }
    }
}