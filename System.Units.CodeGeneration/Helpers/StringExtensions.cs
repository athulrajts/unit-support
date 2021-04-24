namespace System.Units.CodeGeneration
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Returns true if string is not null and not whitespace.
        /// </summary>
        public static bool HasText(this string str) => !string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// Example: "Kilo" + ToCamelCase("NewtonPerMeter") => "KilonewtonPerMeter"
        /// </summary>
        public static string ToCamelCase(this string str)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
