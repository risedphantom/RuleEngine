namespace RuleEngine.Model.Locators
{
    public class AnyLocator : Locator
    {
        public override LocatorType Type => LocatorType.Any;

        public override bool IsLambda => true;
    }
}