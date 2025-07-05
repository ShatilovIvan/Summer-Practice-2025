namespace FileSystemCommands;

public class DirectorySizeCommand : CommandLib.ICommand
{
    public string Path { get; }
    public long Size { get; private set; }

    public DirectorySizeCommand(string path)
    {
        Path = path;
    }
    public void Execute()
    {
        if (!Directory.Exists(Path))
            throw new DirectoryNotFoundException($"The directory '{Path}' does not exist.");

        var directoryInfo = new DirectoryInfo(Path);
        var files = directoryInfo.EnumerateFiles().ToList();

        Size = files.Select(file => file.Length).Sum();
    }
}
