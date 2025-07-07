using PluginLib;

namespace Plugins;

[PluginLoad(["PluginOne"])]
public class PluginTwo : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin Two Executed");
    }
}
