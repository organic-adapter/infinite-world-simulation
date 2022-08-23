namespace AEW.Common.Extensions
{
	public static class StringExtensions
	{
		public static string ThrowIfNullOrEmpty(this string? value)
		{
			return string.IsNullOrEmpty(value) ? throw new StringIsNullOrEmpty() : (string)value;
		}

		public class StringIsNullOrEmpty : Exception
		{
		}
	}
}