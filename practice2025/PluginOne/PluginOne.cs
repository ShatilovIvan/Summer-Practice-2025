using PluginLib;

namespace Plugins;

[PluginLoad(["PluginThree"])]
public class PluginOne : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin One Executed");
    }
}
