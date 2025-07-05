using CommandRunner;
using FileSystemCommands;
using System.Diagnostics;

namespace task08tests;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (baseDir == null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");

        var testDir = Path.Combine(baseDir, "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new DirectorySizeCommand(testDir);

        command.Execute();

        Directory.Delete(testDir, true);

        Assert.Equal(10, command.Size);
    }

    [Fact]
    public void DirectorySizeCommand_ShouldThrowException_WhenDirectoryDoesNotExist()
    {
        string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (baseDir == null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");

        var nonExistentDir = Path.Combine(baseDir, "NonExistentDir");
        var command = new DirectorySizeCommand(nonExistentDir);

        Assert.Throws<DirectoryNotFoundException>(() => command.Execute());
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (baseDir == null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");

        var testDir = Path.Combine(baseDir, "TestDir");
        Directory.CreateDirectory(testDir);

        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");

        command.Execute(); 

        Directory.Delete(testDir, true);

        Assert.Single(command.Files);
        Assert.Contains(Path.Combine(testDir, "file1.txt"), command.Files);
    }

    [Fact]
    public void FindFilesCommand_ShouldThrowException_WhenDirectoryDoesNotExist()
    {
        string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (baseDir == null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");

        var nonExistentDir = Path.Combine(baseDir, "NonExistentDir");
        var command = new FindFilesCommand(nonExistentDir, "*.txt");

        Assert.Throws<DirectoryNotFoundException>(() => command.Execute());
    }
}

public class CommandRunnerTests
{
    [Fact]
    public void Main_ShouldRunCommandsSuccessfully()
    {
        string baseDir = Directory.GetCurrentDirectory();
        var commandRunner = Path.Combine(baseDir, "CommandRunner");

        Console.WriteLine(commandRunner);

        using Process proc = new Process();
        proc.StartInfo.FileName = commandRunner;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start();
        proc.WaitForExit();

        string output = proc.StandardOutput.ReadToEnd();

        Assert.Contains("10", output);
        Assert.Contains("/home/runner/work/Summer-Practice-2025/Summer-Practice-2025/practice2025/TestDir/test1.txt", output);
        Assert.Contains("/home/runner/work/Summer-Practice-2025/Summer-Practice-2025/practice2025/TestDir/test2.txt", output);
    }
}
