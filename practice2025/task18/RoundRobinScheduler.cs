namespace task18;

public class RoundRobinScheduler : IScheduler
{
    private readonly Queue<CommandLib.ICommand> _commands = new();

    public bool HasCommand() => _commands.Count > 0;

    public CommandLib.ICommand Select()
    {
        if (_commands.Count == 0) return null;
        var cmd = _commands.Dequeue();
        _commands.Enqueue(cmd);
        return cmd;
    }

    public void Add(CommandLib.ICommand cmd)
    {
        _commands.Enqueue(cmd);
    }
}
