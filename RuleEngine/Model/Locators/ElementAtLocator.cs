namespace RuleEngine.Model.Locators
{
    public class ElementAtLocator : Locator
    {
        public ElementAtLocator()
        {
        }

        public ElementAtLocator(int index)
        {
            Index = index;
        }

        public int Index { get; set; }

        public override LocatorType Type => LocatorType.ElementAt;

        public override bool IsLambda => true;
    }
}