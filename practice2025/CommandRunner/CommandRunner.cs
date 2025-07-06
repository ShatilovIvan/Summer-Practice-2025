namespace CommandRunner;

using System.Reflection;

public class CommandRunner
{
    public static void Main()
    {
        string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (baseDir is null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");

        string? DllPath = Directory.GetFiles(baseDir, "FileSystemCommands.dll", SearchOption.AllDirectories).FirstOrDefault();

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
        if (directorySizeCommand is null)
            throw new TypeLoadException("Couldn't load type 'FileSystemCommands.DirectorySizeCommand'.");

        var directorySizeCommandInstance = Activator.CreateInstance(directorySizeCommand, new object[] { testDir });
        directorySizeCommandInstance?.GetType().GetMethod("Execute")?.Invoke(directorySizeCommandInstance, null);
        var size = directorySizeCommandInstance?.GetType().GetProperty("Size")?.GetValue(directorySizeCommandInstance, null);

        Console.WriteLine(size);

        var findFilesCommand = assembly.GetType("FileSystemCommands.FindFilesCommand");
        if (findFilesCommand is null)
            throw new TypeLoadException("Couldn't load type 'FileSystemCommands.FindFilesCommand'.");

        var findFilesCommandInstance = Activator.CreateInstance(findFilesCommand, new object[] { testDir, mask });
        findFilesCommandInstance?.GetType().GetMethod("Execute")?.Invoke(findFilesCommandInstance, null);
        var files = findFilesCommandInstance?.GetType().GetProperty("Files")?.GetValue(findFilesCommandInstance, null) as List<string>;

        files?.ForEach(file => Console.WriteLine(file));

        Directory.Delete(testDir, true);
    }
}
