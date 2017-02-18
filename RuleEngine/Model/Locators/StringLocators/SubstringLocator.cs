namespace RuleEngine.Model.Locators.StringLocators
{
    public class SubstringLocator : Locator
    {
        public SubstringLocator()
        {
        }

        public SubstringLocator(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }

        public int Offset { get; set; }

        public int Length { get; set; }

        public override LocatorType Type => LocatorType.Substring;

        public override bool IsLambda => false;
    }
}