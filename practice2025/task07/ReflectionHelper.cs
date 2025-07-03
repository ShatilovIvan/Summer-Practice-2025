namespace task07;

using System.Reflection;

public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();

        if (displayName != null)
            Console.WriteLine($"Class Name: {displayName.DisplayName}");

        if (version != null)
            Console.WriteLine($"Class version: {version.Major}.{version.Minor}");

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ToList();

        if (methods.Any())
        {
            Console.Write("Methods: ");

            foreach (var method in methods)
            {
                var methodDisplayName = method.GetCustomAttribute<DisplayNameAttribute>();

                if (methodDisplayName != null)
                {
                    Console.WriteLine($"{methodDisplayName.DisplayName}");
                }
            }
        }

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).ToList();

        if (props.Any())
        {
            Console.Write("Properties: ");

            foreach (var prop in props)
            {
                var propDisplayName = prop.GetCustomAttribute<DisplayNameAttribute>();

                if (propDisplayName != null)
                {
                    Console.WriteLine($"{propDisplayName.DisplayName}");
                }
            }
        }
    }
}
