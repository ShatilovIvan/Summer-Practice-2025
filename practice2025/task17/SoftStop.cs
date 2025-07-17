using CommandLib;

namespace task17;

public class SoftStop : ICommand
{
    private readonly ServerThread _serverThread;

    public SoftStop(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if (!_serverThread.IsCurrentThread())
            throw new WrongThreadException("SoftStop must be executed in the server thread.");

        _serverThread.RequestSoftStop();
    }
}
