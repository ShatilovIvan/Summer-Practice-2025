namespace task04tests;

using Xunit;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        Cruiser cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
        Assert.Equal(0, cruiser.Position);
        Assert.Equal(0, cruiser.Angle);
        Assert.Equal(0, cruiser.DamageDone);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        Fighter fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower);
        Assert.Equal(0, fighter.Position);
        Assert.Equal(0, fighter.Angle);
        Assert.Equal(0, fighter.DamageDone);
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
        double curPos = fighter.Position;
        fighter.MoveForward();
        double newPosExpect = curPos + fighter.Speed;
        Assert.Equal(100, newPosExpect);
    }

    [Fact]
    public void Cruiser_MovesCorrectly()
    {
        var cruiser = new Cruiser();
        double curPos = cruiser.Position;
        cruiser.MoveForward();
        double newPosExpect = curPos + cruiser.Speed;
        Assert.Equal(50, newPosExpect);
    }

    [Fact]
    public void Fighter_ShootsCorrectly()
    {
        var fighter = new Fighter();
        double curDamage = fighter.DamageDone;
        fighter.Fire();
        double newDamageExpect = curDamage + fighter.FirePower;
        Assert.Equal(50, newDamageExpect);
    }

    [Fact]
    public void Cruiser_ShootsCorrectly()
    {
        var cruiser = new Cruiser();
        double curDamage = cruiser.DamageDone;
        cruiser.Fire();
        double newDamageExpect = curDamage + cruiser.FirePower;
        Assert.Equal(100, newDamageExpect);
    }

    [Fact]
    public void Fighter_RotatesCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(45);
        Assert.Equal(45, fighter.Angle);
    }

    [Fact]
    public void Cruiser_RotatesCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(45);
        Assert.Equal(45, cruiser.Angle);
    }
}
