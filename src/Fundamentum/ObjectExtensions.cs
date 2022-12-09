using System.Text.Json;

namespace Fundamentum;

public static class ObjectExtensions
{
    public static bool IsDefaultOfType<T>(this T @this)
    {
        return EqualityComparer<T>.Default.Equals(@this, default);
    }
    
    public static void Dump<T>(this T @this, Action<string>? writer = null)
    {
        writer ??= (Console.WriteLine);
        
        var output = JsonSerializer.Serialize(@this, new JsonSerializerOptions {WriteIndented = true});

        writer(output);
    }
}