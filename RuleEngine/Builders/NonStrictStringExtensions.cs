using System;

namespace RuleEngine.Builders
{
	public static class NonStrictStringExtensions
	{
		public static bool EndsWith(this string source, string substring)
		{
			return source != null && substring != null && substring != "" && source.EndsWith(substring);
		}

		public static bool StartsWith(this string source, string substring)
		{
			return source != null && substring != null && substring != "" && source.StartsWith(substring);
		}

		public static bool Contains(this string source, string substring)
		{
			return source != null && substring != null && substring != "" && source.Contains(substring);
		}

		public static string Substring(this string source, int startIndex, int length)
		{
			if (source == null || source.Length + startIndex < 0)
				return "";

			var offset = startIndex >= 0 ? startIndex : source.Length + startIndex;
			return source.Length < offset + length ? "" : source.Substring(offset, length);
		}

		public static string Reverse(this string source)
		{
			if (source == null)
				return "";

			var chars = source.ToCharArray();
			Array.Reverse(chars);
			return new string(chars);
		}

		public static int Length(this string source)
		{
			return source?.Length ?? 0;
		}
	}
}
