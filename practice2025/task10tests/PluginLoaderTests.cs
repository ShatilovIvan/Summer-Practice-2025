namespace task10tests;

using task10;

public class PluginLoaderTests
{
    [Fact]
    public void PluginLoader_ExecutePluginsInCorrectOrder()
    {
        var slnDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

        if (slnDirectory is null)
            throw new DirectoryNotFoundException("Couldn't find base directory!\n");

        var stringWriter = new StringWriter();
        var expectedOutput = "Plugin Three Executed\nPlugin One Executed\nPlugin Two Executed";
        Console.SetOut(stringWriter);

        var pluginsDirectory = Path.Combine(slnDirectory, "PluginsCompiled");
        PluginLoader.LoadPlugins(pluginsDirectory);

        Assert.Contains(expectedOutput, stringWriter.ToString());
    }
}
