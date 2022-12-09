namespace Fundamentum.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("Foo", "foo", true)]
    [InlineData("Foo", "Foo", true)]
    [InlineData("Foo", "FoO", true)]
    [InlineData("Foo", "bar", false)]
    [InlineData("Foo bar", "bar", true)]
    [InlineData("Foo bar", "Bar", true)]
    [InlineData("Foo bar", " Bar", true)]
    [InlineData("Foo bar", " tar", false)]
    public void ContainsInsensitive(string value, string compare, bool result)
    {
        value.ContainsInsensitive(compare)
            .Should()
            .Be(result);
    }
}