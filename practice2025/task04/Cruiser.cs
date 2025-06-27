namespace task04;

public class Cruiser : ISpaceship
{
    public int Speed { get; } = 50;
    public int FirePower { get; } = 100;

    public string MoveForward()
    {
        return $"Корабль движется со скоростью {Speed} м/c.";
    }

    public string Rotate(int angle)
    {
        return $"Корабль повернулся на {angle} градусов.";
    }

    public string Fire()
    {
        return $"Корабль нанес {FirePower} единиц урона.";
    }
}