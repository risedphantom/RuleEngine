using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RuleEngine.Model;

namespace RuleEngine.Serialization
{
    public class RuleSerializer
    {
	    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
	    {
		    Formatting = Formatting.Indented,
		    NullValueHandling = NullValueHandling.Ignore,
		    DefaultValueHandling = DefaultValueHandling.Ignore,
		    Converters = new List<JsonConverter>
		    {
			    new StringEnumConverter(),
			    new LocatorConverter()
		    }
	    };

        public string SerializeRules<T>(IList<T> rules) where T : IRule
        {
            var result = JsonConvert.SerializeObject(rules, _settings);
            return result;
        }

		public string SerializeRule<T>(T rule) where T : IRule
		{
			var result = JsonConvert.SerializeObject(rule, _settings);
			return result;
		}

		public IList<T> DeserializeRules<T>(string resultJson) where T : IRule
        {
            return JsonConvert.DeserializeObject<List<T>>(resultJson, _settings);
        }

		public T DeserializeRule<T>(string resultJson) where T : IRule
		{
			return JsonConvert.DeserializeObject<T>(resultJson, _settings);
		}
	}
}