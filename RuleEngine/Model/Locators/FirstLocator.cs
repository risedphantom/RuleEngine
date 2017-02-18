namespace RuleEngine.Model.Locators
{
    public class FirstLocator : Locator
    {
        public override LocatorType Type => LocatorType.First;

        public override bool IsLambda => true;
    }
}