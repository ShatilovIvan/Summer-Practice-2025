namespace PluginLib;

[AttributeUsage(AttributeTargets.Class)]
public class PluginLoad : Attribute
{
    public List<string> Dependencies { get; private set; }

    public PluginLoad()
        => Dependencies = [];

    public PluginLoad(string[] dependencies)
        => Dependencies = new List<string>(dependencies);
}
