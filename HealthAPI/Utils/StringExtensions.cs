namespace HealthAPI.Utils
{
    public static class StringExtensions
    {
        public static string FirstLetterToCaps(this string value)
        {
            if (value.Length == 0)
                return value;
            else if (value.Length == 0)
                return char.ToUpper(value[0]).ToString();
            else
                return $"{char.ToUpper(value[0])}{value.Substring(1)}";
        }
    }
}
