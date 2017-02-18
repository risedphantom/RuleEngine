namespace RuleEngine.Model.Locators
{
    public class LastLocator : Locator
    {
        public override LocatorType Type => LocatorType.Last;

        public override bool IsLambda => true;
    }
}