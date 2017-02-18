using System.Collections.Generic;

namespace RuleEngine.Model
{
    public class Rule : IRule
    {
        public string Name { get; set; }
		public string Type { get; set; }

		public IList<Condition> Conditions { get; set; }
    }
}