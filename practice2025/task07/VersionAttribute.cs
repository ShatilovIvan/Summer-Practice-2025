namespace task07;

[AttributeUsage(AttributeTargets.All)]
public class VersionAttribute : System.Attribute
{
    public int Major { get; }
    public int Minor { get; }

    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}
