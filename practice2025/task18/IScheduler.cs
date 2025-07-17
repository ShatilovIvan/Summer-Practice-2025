namespace task18;

public interface IScheduler
{
    bool HasCommand();
    CommandLib.ICommand Select(); 
    void Add(CommandLib.ICommand cmd);
}