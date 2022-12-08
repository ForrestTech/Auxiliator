namespace Auxiliator.Tests;

public class CollectionExtensionTests
{
    [Fact]
    public void None()
    {
        (new List<bool>
        {
            true,
            false,
            false
        }).None(x => x).Should().BeFalse();
    }
}