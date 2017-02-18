namespace RuleEngine.Model.Locators
{
	public sealed class MemberLocator : Locator
	{
		public LocatorOperation Operation = LocatorOperation.Equal;

		public MemberLocator()
		{
		}

		public MemberLocator(string property, bool asString = false, LocatorOperation op = LocatorOperation.None) : this()
		{
			Property = property;
			Operation = op;
			AsString = asString;
		}

		public string Property { get; set; }
		public bool AsString { get; set; }

		public override LocatorType Type => LocatorType.Property;
	}
}