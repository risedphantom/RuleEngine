using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RuleEngine.Model;
using RuleEngine.Model.Locators;
using RuleEngine.Model.Locators.StringLocators;

namespace RuleEngine.Serialization
{
    public class LocatorConverter : JsonCreationConverter<Locator>
    {
        public override bool CanWrite => false;

        protected override Locator Create(Type objectType, JObject jObject, JsonSerializer serializer)
        {
            var type = GetlocatorType(jObject);
            switch (type)
            {
                case LocatorType.Property:
                    return new MemberLocator();
                case LocatorType.Constant:
                    return new ConstantLocator();
                case LocatorType.Any:
                    return new AnyLocator();
                case LocatorType.First:
                    return new FirstLocator();
                case LocatorType.Predicate:
                    return new PredicateLocator();
                case LocatorType.Last:
                    return new LastLocator();
                case LocatorType.ElementAt:
                    return new ElementAtLocator();
                case LocatorType.Substring:
                    return new SubstringLocator();
                case LocatorType.StartWith:
                    return new StartsWithLocator();
                case LocatorType.EndWith:
                    return new EndWithLocator();
                case LocatorType.Concat:
                    return new ConcatLocator();
                case LocatorType.RegEx:
                    return new RegExLocator();
                case LocatorType.Contains:
                    return new ContainsLocator();
				case LocatorType.Reverse:
					return new ReverseLocator();
				case LocatorType.Length:
					return new LengthLocator();
				default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private LocatorType GetlocatorType(JObject jObject)
        {
            var type = jObject["Type"];
            if (type == null)
            {
                return default(LocatorType);
            }
            return (LocatorType) type.ToObject(typeof(LocatorType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }

    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject, JsonSerializer serializer);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(objectType, jObject, serializer);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }
    }
}