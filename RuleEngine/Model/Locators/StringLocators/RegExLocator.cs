namespace RuleEngine.Model.Locators.StringLocators
{
    public class RegExLocator : Locator
    {
        public RegExLocator(string regEx)
        {
            RegEx = regEx;
        }

        public RegExLocator()
        {
        }

        public override LocatorType Type => LocatorType.RegEx;

        public string RegEx { get; set; }
    }
}