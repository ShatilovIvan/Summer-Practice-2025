using CommandLib;
namespace task18;

public class MultiStepCommand : ICommand
{
    private readonly int _id;
    private int _counter = 0;
    private readonly int _maxCount;
    private readonly IScheduler _scheduler;

    public MultiStepCommand(int id, int maxCount, IScheduler scheduler)
    {
        _id = id;
        _maxCount = maxCount;
        _scheduler = scheduler;
    }

    public void Execute()
    {
        if (_counter < _maxCount)
        {
            Console.WriteLine($"Thread {_id} call {(_counter++)}");
            
            if (_counter < _maxCount)
            {
                _scheduler.Add(this);
            }
        }
    }
}