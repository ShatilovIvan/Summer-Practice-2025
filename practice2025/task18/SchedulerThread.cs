using System.Collections.Concurrent;
using CommandLib;
using task17;

namespace task18;

public class SchedulerThread
{
    private readonly BlockingCollection<CommandLib.ICommand> _queue = new();
    private readonly IScheduler _scheduler;
    private readonly Thread _thread;
    private bool _softStopRequested = false;
    private bool _hardStopRequested = false;

    public SchedulerThread(IScheduler scheduler)
    {
        _scheduler = scheduler;
        _thread = new Thread(HandleCommands) { IsBackground = true };
    }

    public void Start() => _thread.Start();

    public void AddCommand(CommandLib.ICommand command) => _queue.Add(command);

    public void RequestSoftStop() => _softStopRequested = true;
    public void RequestHardStop() => _hardStopRequested = true;

    private void HandleCommands()
    {
        while (!_hardStopRequested)
        {
            if (_scheduler.HasCommand())
            {
                var cmd = _scheduler.Select();

                try
                {
                    cmd.Execute();
                }

                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex, cmd);
                }

                continue;
            }

            CommandLib.ICommand command;

            if (_softStopRequested && _queue.Count == 0)
                break;

            if (_queue.TryTake(out command, 100))
            {
                try
                {
                    command.Execute();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex, command);
                }
            }
            else
            {
                Thread.Sleep(10);
            }
        }
    }
}
