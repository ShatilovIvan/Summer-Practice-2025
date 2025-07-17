namespace task17;

public class WrongThreadException : Exception
{
    public WrongThreadException(string message) : base(message) { }
}
