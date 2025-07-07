using PluginLib;

namespace Plugins;

[PluginLoad()]
public class PluginThree : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin Three Executed");
    }
}
