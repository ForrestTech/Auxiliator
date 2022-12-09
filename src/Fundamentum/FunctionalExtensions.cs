namespace Fundamentum;

public static class FunctionalExtensions
{
    public static TOutput Alt<TInput, TOutput>(this TInput @this, Func<TInput, TOutput> firstAlt, Func<TInput, TOutput> secondAlt)
    {
        var first = firstAlt(@this);
        return EqualityComparer<TOutput>.Default.Equals(first, default) ? secondAlt(@this) : first;
    }

    private static TOutput IfDefaultDo<TInput, TOutput>(this TOutput @this, Func<TInput, TOutput> elseIf, TInput input) =>
        EqualityComparer<TOutput>.Default.Equals(@this, default)
            ? elseIf(input)
            : @this;
    
    public static T AggregateFunc<T>(this IEnumerable<Func<T, T>> @this, T seed = default) where T: struct => 
        @this.Aggregate(seed, (curr, next) => next(curr));
    
    
    public static T IfThen<T>(this T @this, Func<T, bool> predicate, Func<T, T> doFunc) =>
        predicate(@this)
            ? doFunc(@this)
            : @this;
    
    public static TOutput Fork<TInput, TOutput>(this TInput @this, Func<IEnumerable<TOutput>, TOutput> joinFunc, params Func<TInput, TOutput>[] prongs) =>
        prongs.Select(x => x(@this)).Map(joinFunc);

    public static TToType Map<TFromType, TToType>(this TFromType @this, Func<TFromType, TToType> mapFunc) =>
        mapFunc(@this);
    
    public static TOutput Match<TInput, TOutput>(this TInput @this, params 
        (Func<TInput, bool> condition, 
        Func<TInput, TOutput> translation)[] matchFunctionPairs) =>
        matchFunctionPairs.First(x => x.condition(@this)).translation(@this);

    public static IEnumerable<T> Repeat<T>(this Func<T> @this, int times) =>
        Enumerable.Repeat(@this, times)
            .Select(x => x());

    public static IEnumerable<TOutput> Repeat<TInput, TOutput>(this TInput @this, Func<TInput, TOutput> f, int times) =>
        Enumerable.Repeat(@this, times)
            .Select(f);
    
    public static bool Validate<TInput>(this TInput @this, params Func<TInput, bool>[] predicates) =>
        predicates.All(x => x(@this));

    public static T WhileDo<T>(this T @this, Func<T, bool> predicate, Func<T, T> updateFunc)
    {
        while (true)
        {
            if (!predicate(@this))
            {
                return @this;
            }

            @this = updateFunc(@this);
        }
    }
}