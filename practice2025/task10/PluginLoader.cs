using System.Reflection;
using PluginLib;

namespace task10;

public class PluginLoader
{
    private static List<Type> _loadedPlugins = new();
    
    public static void LoadPlugins(string? pluginsPath)
    {
        if (pluginsPath is null)
            throw new DirectoryNotFoundException("Couldn't find plugins directory!\n");

        var assemblies = Directory.GetFiles(pluginsPath, "*.plugin").Select(Assembly.LoadFrom).SelectMany(s => s.GetTypes()).ToList();
        var plugins = assemblies.Where(s => s.GetCustomAttribute<PluginLoad>() is not null).ToList();

        plugins.ForEach(s => LoadPlugin(s, plugins));
    }

    private static void LoadPlugin(Type curPlugin, List<Type> allPlugins)
    {
        if (_loadedPlugins.Contains(curPlugin))
            return;

        var dependencies = curPlugin.GetCustomAttribute<PluginLoad>()?.Dependencies;

        if (dependencies is not null && dependencies.Any())
            dependencies.Select(depName => allPlugins.FirstOrDefault(s => s.Name == depName)).ToList().ForEach(depType => LoadPlugin(depType, allPlugins));

        _loadedPlugins.Add(curPlugin);
        var plugin = (IPlugin)Activator.CreateInstance(curPlugin);
        plugin.Execute();
    }
}
