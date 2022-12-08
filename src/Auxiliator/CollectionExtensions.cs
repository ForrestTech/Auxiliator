namespace Auxiliator;

public static class CollectionExtensions
{
    public static IEnumerable<T> AsArray<T>(this T @this) => new[] {@this};
    
    public static IEnumerable<T> DistinctBy<T, K>(this IEnumerable<T> @this, Func<T, K> keySelector)
    {
        var seenKeys = new HashSet<K>();

        return @this.Where(element => seenKeys.Add(keySelector(element)));
    }
    
    /// <summary>
    /// Determines whether all elements of a sequence do not satisfy a condition.
    /// The inverse of All LINQ expression, so you can write some predicates as in the positive form with None 
    /// </summary>
    /// <param name="this">An IEnumerable T that contains the elements to apply the predicate to.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="T">TSource – The type of the elements of source.</typeparam>
    /// <returns>True if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.</returns>
    public static bool None<T>(this IEnumerable<T> @this, Func<T, bool> predicate)
    {
        return @this.All(element => !predicate(element));
    }
    
    /// <summary>
    /// Determines if the collection is empty
    /// </summary>
    /// <param name="this">The IEnumerable of T to test </param>
    /// <typeparam name="T">TSource – The type of the elements of source.</typeparam>
    /// <returns>True if the enumerable has no items in it; otherwise, false.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> @this)
    {
        return !@this.Any();
    }
    
    /// <summary>
    /// Tries to get the element at the specified index in the array if that element does not exist return the default value 
    /// </summary>
    /// <param name="this">The array of T to test</param>
    /// <param name="index">The index to try and return</param>
    /// <typeparam name="T">TSource – The type of the elements of source.</typeparam>
    /// <returns>The element at the specified index or null</returns>
    public static T? TryGetElement<T>(this T[] @this, int index) where T : class
    {
        if (index < 0 || index >= @this.Length)
        {
            return default;
        }

        return @this[index];
    }
}