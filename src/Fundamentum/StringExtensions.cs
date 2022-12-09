namespace Fundamentum;

public static class StringExtensions
{
    /// <summary>
    ///   Returns a value indicating whether a specified string occurs within this string ignoring culture and case.
    /// </summary>
    /// <param name="this">The current string.</param>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value parameter occurs within this string; otherwise, false.</returns>
    public static bool ContainsInsensitive(this string @this, string value)
    {
        return @this.Contains(value, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   Formats the source text using the specified parameters
    /// </summary>
    /// <param name="this">The current string</param>
    /// <param name="arguments">Arguments to be passed to format function</param>
    /// <returns>Formatted string</returns>
    public static string Format(this string @this, params object[] arguments)
    {
        return string.Format(@this, arguments);
    }

    /// <summary>
    ///   Returns a value indicating whether a specified string contains any text.
    /// </summary>
    /// <param name="this">The string to test</param>
    /// <returns>True if the text parameter contains is not null and contains a value that is not whitespace; otherwise, false.</returns>
    public static bool IsSomething(this string @this)
    {
        return !string.IsNullOrWhiteSpace(@this);
    }

    /// <summary>
    ///   Returns a value indicating whether a specified string is null or empty
    /// </summary>
    /// <param name="this">The string to test</param>
    /// <returns>True if the text parameter is null or contains only whitespace; otherwise, false.</returns>
    public static bool IsEmpty(this string @this)
    {
        return string.IsNullOrWhiteSpace(@this);
    }


    /// <summary>
    ///   Determines if two string values are not equal based on the string comparison
    /// </summary>
    /// <param name="this">The current string</param>
    /// <param name="value">The value to check</param>
    /// <param name="comparison">String comparison to use. Defaults to (InvariantCultureIgnoreCase)</param>
    /// <returns></returns>
    public static bool NotEqual(this string @this, string value,
        StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        return !string.Equals(@this, value, comparison);
    }
    
    public static IEnumerable<string> SplitOnNewline(this string @this) =>
        @this.Split(Environment.NewLine);

    public static IEnumerable<string> SplitOnSpace(this string @this) =>
        @this.Split(" ");
}