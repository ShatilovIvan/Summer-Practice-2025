namespace task17;

public static class ExceptionHandler
{
    public static void Handle(Exception ex, CommandLib.ICommand command)
    {
        Console.WriteLine($"Exception in command {command.GetType().Name}: {ex.Message}");
    }
}
