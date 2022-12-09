namespace Fundamentum;

public static class NumberExtensions
{
    public static bool IsDivisibleBy(this int @this, int divisor) =>
        @this % divisor == 0;
}