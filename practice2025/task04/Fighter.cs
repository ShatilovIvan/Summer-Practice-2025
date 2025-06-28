namespace task04;

public class Fighter : ISpaceship
{
    public int Speed { get; } = 100;
    public int FirePower { get; } = 50;
    public double Position { get; set; } = 0;
    public double Angle { get; set; } = 0;
    public double DamageDone { get; set; } = 0;

    public void MoveForward()
    {
        Position += Speed;
    }

    public void Rotate(int angle)
    {
        Angle = (Angle + angle) % 360;
    }

    public void Fire()
    {
        DamageDone += FirePower;
    }
}
