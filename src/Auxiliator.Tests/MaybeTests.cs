namespace Auxiliator.Tests;

public class MaybeTests
{
    [Fact]
    public void Two_nones_are_equal()
    {
        var one = Maybe<int>.None;
        var two = Maybe<int>.None;

        one.Should().Be(two);
        one.Should().BeEquivalentTo(two);
    }
    
    [Fact]
    public void Two_some_of_the_same_value_are_equal()
    {
        var one = Maybe<int>.Some(1);
        var two = Maybe<int>.Some(1);

        one.Should().Be(two);
        one.Should().BeEquivalentTo(two);
    }
    
    [Fact]
    public void None_and_some_not_equal()
    {
        var one = Maybe<int>.None;
        var two = Maybe<int>.Some(1);

        one.Should().NotBe(two);
        one.Should().NotBeEquivalentTo(two);
    }
    
    [Fact]
    public void Two_some_of_different_values_not_equal()
    {
        var one = Maybe<int>.Some(1);
        var two = Maybe<int>.Some(2);

        one.Should().NotBe(two);
        one.Should().NotBeEquivalentTo(two);
    }
    
    [Fact]
    public void None_constructor_should_be_none()
    {
        var sut = Maybe<int>.None;

        sut.IsNone().Should().BeTrue();
    }

    [Fact]
    public void Some_constructor_should_be_none()
    {
        var sut = Maybe<int>.Some(1);

        sut.IsSomething().Should().BeTrue();
    }

    [Fact]
    public void Match_on_some_should_call_some_function()
    {
        var sut = Maybe<int>.Some(1);
        var actual = sut.Match(v => v + 1, () => 0);

        actual.Should().Be(2);
    }

    [Fact]
    public void Match_on_none_should_call_some_function()
    {
        var sut = Maybe<int>.None;
        var actual = sut.Match(v => v + 1, () => 0);

        actual.Should().Be(0);
    }

    [Fact]
    public void Map_on_some_should_return_maybe_of_some_function()
    {
        var sut = Maybe<int>.Some(1);
        var actual = sut.Map(v => v + 1);

        actual.GetOrDefault(0).Should().Be(2);
    }

    [Fact]
    public void Map_on_none_should_return_maybe_of_some_function()
    {
        var sut = Maybe<int>.None;
        var actual = sut.Map(v => v + 1);

        actual.GetOrDefault(0).Should().Be(0);
    }


    record Person(string FirstName, string LastName, int Age);

    Func<int, Person> GetEmptyPerson = (int personId) =>
        null!;

    [Fact]
    public void Bind_no_value()
    {
        var personId = 12;
        string? formattedPerson = personId.ToMaybe()
            .Bind(GetEmptyPerson)
            .Bind(x => $"{x.FirstName} {x.LastName} ({x.Age})")
            .Bind(x => x.Replace("a", "4"))
            .Bind(x => x.Replace("e", "3"))
            .Bind(x => x.Replace("i", "1"))
            .Bind(x => x.Replace("o", "0"));

        formattedPerson.Should().BeNull();
    }
    
    Func<int, Person> GetPerson = (int personId) =>
        new Person("Simon","Painter",36);
    
    [Fact]
    public void Bind_with_value_executes_binds()
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

}