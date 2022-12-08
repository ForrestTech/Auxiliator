namespace Auxilium;

public abstract record Either<T>
{
    public abstract T? Value { get; }
    public static implicit operator T?(Either<T> @this) => @this.Value;
}

public record Right<T>(T Value) : Either<T>
{
    public override T Value { get; } = Value;
}

public record Left<T>(Exception Exception) : Either<T>
{
    public override T? Value => default;
    public Exception Exception { get; } = Exception;
}

public static class EitherExtensions
{
    public static Either<T> ToEither<T>(this T @this) => new Right<T>(@this);

    public static Either<R> Bind<T, R>(this Either<T> @this, Func<T, R> mapFunction)
    {
        switch (@this)
        {
            case Right<T> right when !EqualityComparer<T>.Default.Equals(right.Value, default):
                try
                {
                    return mapFunction(right.Value).ToEither();
                }
                catch (Exception e)
                {
                    return new Left<R>(e);
                }
            case Left<T> left:
                return new Left<R>(left.Exception);
            default:
                return new Left<R>(new Exception("Default value"));
        }
    }
}