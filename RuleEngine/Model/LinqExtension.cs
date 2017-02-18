using System;

namespace RuleEngine.Model
{
    public static class LinqExtension
    {
        public static bool Predicate<T>(this T value, Func<T, bool> predicate)
        {
            return predicate(value);
        }
    }
}