namespace task04tests;

using Xunit;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Fighter_ShouldBeWeakerThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.FirePower < cruiser.FirePower);
    }

    [Fact]
    public void Fighter_MovesCorrectly()
    {
        var fighter = new Fighter();
        string result = fighter.MoveForward();
        Assert.Equal("Корабль движется со скоростью 100 м/c.", result);
    }

    [Fact]
    public void Cruiser_MovesCorrectly()
    {
        var cruiser = new Cruiser();
        string result = cruiser.MoveForward();
        Assert.Equal("Корабль движется со скоростью 50 м/c.", result);
    }

    [Fact]
    public void Fighter_ShootsCorrectly()
    {
        var fighter = new Fighter();
        string result = fighter.Fire();
        Assert.Equal("Корабль нанес 50 единиц урона.", result);
    }

    [Fact]
    public void Cruiser_ShootsCorrectly()
    {
        var cruiser = new Cruiser();
        string result = cruiser.Fire();
        Assert.Equal("Корабль нанес 100 единиц урона.", result);
    }
}