using System.Collections.Concurrent;

namespace task17;

public class ServerThread
{
    private readonly BlockingCollection<CommandLib.ICommand> _queue = new();
    private readonly Thread _thread;
    private bool _softStopRequested = false;
    private bool _hardStopRequested = false;

    public ServerThread()
    {
        _thread = new Thread(HandleCommands) { IsBackground = true };
    }

    public void Start() => _thread.Start();

    public void AddCommand(CommandLib.ICommand command) => _queue.Add(command);

    public void RequestSoftStop()
    {
        _softStopRequested = true;
    }

    public void RequestHardStop()
    {
        _hardStopRequested = true;
    }

    public bool IsCurrentThread() => Thread.CurrentThread == _thread;

    private void HandleCommands()
    {
        while (!_hardStopRequested)
        {
            if (_softStopRequested && !_queue.Any())
                break;

            CommandLib.ICommand command = _queue.Take();

            try
            {
                command.Execute();
            }

            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, command);
            }
        }
    }
}
