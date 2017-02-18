namespace RuleEngine.Model.Locators
{
	public class ConstantLocator : Locator
	{
		public ConstantLocator()
		{
		}

		public ConstantLocator(object value, bool tryCast = false) : this()
		{
			Value = value;
			TryCast = tryCast;
		}

		public object Value { get; set; }
		public bool TryCast { get; set; }

		public override LocatorType Type => LocatorType.Constant;
	}
}