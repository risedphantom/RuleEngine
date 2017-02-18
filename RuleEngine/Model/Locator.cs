using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleEngine.Model
{
    public abstract class Locator
    {
        public Locator Left { get; set; }

        public Locator Right { get; set; }

        public IList<Condition> Conditions { get; set; }

        public abstract LocatorType Type { get; }

        [JsonIgnore]
        public virtual bool IsLambda => false;
    }
}