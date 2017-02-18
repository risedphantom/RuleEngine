namespace RuleEngine.Model.Locators
{
    public class PredicateLocator : Locator
    {
        public override LocatorType Type => LocatorType.Predicate;

        public override bool IsLambda => true;
    }
}