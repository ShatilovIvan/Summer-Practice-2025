namespace CommandRunner;

using System.Reflection;

class Program
{
    public static void Main()
    {
        string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (baseDir is null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");
        

        string? DllPath = Directory.GetFiles(baseDir, "FileSystemCommands.dll", SearchOption.AllDirectories).First();

        if (DllPath is null)
            throw new FileNotFoundException("Couldn't find dynamic library!\n");

        var testDir = Path.Combine(baseDir, "TestDir");

        Directory.CreateDirectory(testDir);

        var firstFile = Path.Combine(testDir, "test1.txt");
        var secondFile = Path.Combine(testDir, "test2.txt");
        var mask = "*.txt";

        File.WriteAllText(firstFile, "Hello");
        File.WriteAllText(secondFile, "World");

        var assembly = Assembly.LoadFrom(DllPath);

        var directorySizeCommand = assembly.GetType("FileSystemCommands.DirectorySizeCommand");
        var directorySizeCommandInstance = Activator.CreateInstance(directorySizeCommand, new object[] { testDir });
        directorySizeCommandInstance.GetType().GetMethod("Execute")?.Invoke(directorySizeCommandInstance, null);

        var findFilesCommand = assembly.GetType("FileSystemCommands.FindFilesCommand");
        var findFilesCommandInstance = Activator.CreateInstance(findFilesCommand, new object[] { testDir, mask });
        findFilesCommandInstance.GetType().GetMethod("Execute")?.Invoke(findFilesCommandInstance, null);

        Directory.Delete(testDir, true);

    }
}
