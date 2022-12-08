using System.Text.Json.Serialization;

namespace Auxilium;

public abstract record Maybe<T>
{
    [JsonInclude]
    public abstract T? Value { get; }
    
    public bool IsSomething() => !EqualityComparer<T>.Default.Equals(Value, default);

    public bool IsNone() => !IsSomething();

    public R Match<R>(Func<T, R> someFunc, Func<R> noneFunc)
    {
        return Value != null && IsSomething() ? someFunc(Value) : noneFunc();
    }

    public Maybe<R> Map<R>(Func<T, R> map)
    {
        return Match(
            v => Maybe<R>.Some(map(v))!,
            () => Maybe<R>.None
        );
    }

    public void Iter(Action<T> some, Action none)
    {
        if (Value != null && IsSomething())
        {
            some(Value);
        }
        else
        {
            none();
        }
    }

    public T GetOrDefault(T defaultedValue) => Match(v => v, () => defaultedValue);

    public R GetOrElse<R>(Func<T, R> someFunc, R none) => Match(someFunc, () => none);
    public static Maybe<T> None => new None<T>();
    
    public static Maybe<T> Some(T value) => new Some<T>(value);

    public override string ToString()
    {
        return $"Maybe<{typeof(T).Name}>: [{(IsSomething() ? "Some" : "None")}{(IsSomething() ? $"({Value})" : "")}]";
    }
    
    public static implicit operator T?(Maybe<T> @this) => @this.Value!;
}

public record Some<T>(T Value) : Maybe<T>
{
    public override T Value { get; } = Value;
}

public record None<T> : Maybe<T>
{
    public override T? Value => default;
}

public static class MaybeExtensions
{
    /// <summary>
    /// Tries to get the element at the specified index in the array if that element does not exist return the default value 
    /// </summary>
    /// <param name="source">The array of T to test</param>
    /// <param name="index">The index to try and return</param>
    /// <typeparam name="T">TSource – The type of the elements of source.</typeparam>
    /// <returns>The element at the specified index or null</returns>
    public static Maybe<T> TryGetElement<T>(this T[] source, int index) 
    {
        if (index < 0 || index >= source.Length)
        {
            return Maybe<T>.None;
        }

        return Maybe<T>.Some(source[index]);
    }
    
    public static Maybe<T> ToMaybe<T>(this T value) => Maybe<T>.Some(value);
    
    public static Maybe<R> Bind<T, R>(this Maybe<T> @this, Func<T, R> mapFunction)
    {
        switch (@this)
        {
            case Some<T> some when some.IsSomething():
                try
                {
                    return mapFunction(some!).ToMaybe();
                }
                catch (Exception)
                {
                    return Maybe<R>.None;
                }
            default:
                return Maybe<R>.None;
        }
    }

}