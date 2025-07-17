using CommandLib;

namespace task17;

public class HardStop : ICommand
{
    private readonly ServerThread _serverThread;

    public HardStop(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if (!_serverThread.IsCurrentThread())
            throw new WrongThreadException("HardStop must be executed in the server thread.");

        _serverThread.RequestHardStop();
    }
}
