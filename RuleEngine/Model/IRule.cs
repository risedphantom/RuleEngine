using System.Collections.Generic;

namespace RuleEngine.Model
{
    public interface IRule
    {
        string Name { get; set; }

		string Type { get; set; }

		IList<Condition> Conditions { get; set; }
    }
}