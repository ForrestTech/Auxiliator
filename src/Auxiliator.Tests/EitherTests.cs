namespace Auxiliator.Tests;

public class EitherTests
{
    record Person(string FirstName, string LastName, int Age);

    Func<int, Person> GetPerson = _ => new Person("Simon","Painter",36);
    
    [Fact]
    public void Bind_executes_when_maybe_is_some()
    {
        var personId = 12;
        string? formattedPerson = personId.ToEither()
            .Bind(GetPerson)
            .Bind(x => $"{x.FirstName} {x.LastName} ({x.Age})")
            .Bind(x => x.Replace("a", "4"))
            .Bind(x => x.Replace("e", "3"))
            .Bind(x => x.Replace("i", "1"))
            .Bind(x => x.Replace("o", "0"));

        formattedPerson.Should().Be("S1m0n P41nt3r (36)");
    }


    Func<int, Person> GetPersonException = _ => throw new Exception("Error!");

    [Fact]
    public void Bind_catches_exception_and_returns_left()
    {
        var personId = 12;
        var formattedPerson = personId.ToEither()
            .Bind(GetPersonException)
            .Bind(x => $"{x.FirstName} {x.LastName} ({x.Age})")
            .Bind(x => x.Replace("a", "4"))
            .Bind(x => x.Replace("e", "3"))
            .Bind(x => x.Replace("i", "1"))
            .Bind(x => x.Replace("o", "0"));

        formattedPerson.Should().BeOfType<Left<string>>();
        var left = formattedPerson as Left<string>;
        left?.Exception.Message.Should().Be("Error!");
    }
}