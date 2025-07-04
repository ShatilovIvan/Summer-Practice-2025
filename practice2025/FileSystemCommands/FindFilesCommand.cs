namespace FileSystemCommands
{
    public class FindFilesCommand : CommandLib.ICommand
    {
        public string Path { get; }
        public string Mask { get; }
        public List<string> Files { get; private set; } = [];

        public FindFilesCommand(string path, string mask)
        {
            Path = path;
            Mask = mask;
        }

        public void Execute()
        {
            if (!Directory.Exists(Path))
                throw new DirectoryNotFoundException($"The directory '{Path}' does not exist.");

            Files = Directory.GetFiles(Path, Mask, SearchOption.AllDirectories).ToList();
        }
    }
}
