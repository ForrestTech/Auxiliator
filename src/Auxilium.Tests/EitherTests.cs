using FluentAssertions;

namespace Auxilium.Tests;

public class EitherTests
{
    record Person(string FirstName, string LastName, int Age);

    Func<int, Person> GetPerson = (int personId) =>
        new Person("Simon","Painter",36);
    
    [Fact]
    public void Test01()
    {
        var personId = 12;
        string? formattedPerson = personId.ToMaybe()
            .Bind(GetPerson)
            .Bind(x => $"{x.FirstName} {x.LastName} ({x.Age})")
            .Bind(x => x.Replace("a", "4"))
            .Bind(x => x.Replace("e", "3"))
            .Bind(x => x.Replace("i", "1"))
            .Bind(x => x.Replace("o", "0"));

        formattedPerson.Should().Be("S1m0n P41nt3r (36)");
    }


    Func<int, Person> GetPerson2 = (int personId) => throw new Exception("Arrgh!");

    [Fact]
    public void Test02()
    {
        var personId = 12;
        var formattedPerson = personId.ToEither()
            .Bind(GetPerson2)
            .Bind(x => $"{x.FirstName} {x.LastName} ({x.Age})")
            .Bind(x => x.Replace("a", "4"))
            .Bind(x => x.Replace("e", "3"))
            .Bind(x => x.Replace("i", "1"))
            .Bind(x => x.Replace("o", "0"));

        formattedPerson.Should().BeOfType<Left<string>>();
        var left = formattedPerson as Left<string>;
        left.Exception.Message.Should().Be("Arrgh!");
    }
}