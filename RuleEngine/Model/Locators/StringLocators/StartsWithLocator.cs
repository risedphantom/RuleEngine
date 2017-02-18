namespace RuleEngine.Model.Locators.StringLocators
{
    public class StartsWithLocator : Locator
    {
        public StartsWithLocator()
        {
        }

        public StartsWithLocator(string startWith)
        {
            StartWith = startWith;
        }

        public string StartWith { get; set; }

        public override LocatorType Type => LocatorType.StartWith;
    }
}