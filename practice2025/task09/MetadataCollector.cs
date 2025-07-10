using System.Reflection;
using System.Text;

namespace task09;

public class MetadataCollector
{
    public static void GetMetadata(string path)
    {
        var assembly = Assembly.LoadFrom(path);

        if (assembly is null)
            throw new TypeLoadException("Couldn't load a library");

        var sb = new StringBuilder();

        var types = assembly.GetTypes().ToList();

        foreach (var type in types)
        {
            sb.Append($"Class: {type.Name}\n");

            var attributes = type.GetCustomAttributes(true);
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            if (attributes.Any())
            {
                sb.Append($"\tAttributes: \n");

                foreach (var attribute in attributes)
                {
                    sb.Append($"\t\t{attribute.ToString()}\n");
                }
            }

            if (methods.Any())
            {
                sb.Append($"\tMethods: \n");

                foreach (var method in methods)
                {
                    sb.Append($"\t\t{method.Name}\n");

                    var parameters = method.GetParameters();

                    if (parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            sb.Append($"\t\t\tparameter: {parameter.Name}, type: {parameter.GetType()}\n");
                        }
                    }
                }
            }

            if (constructors.Any())
            {
                sb.Append($"\tConstructors: \n");

                foreach (var constructor in constructors)
                {
                    sb.Append($"\t\t{constructor.Name}\n");

                    var parameters = constructor.GetParameters();

                    if (parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            sb.Append($"\t\t\tparameter: {parameter.Name}, type: {parameter.GetType()}\n");
                        }
                    }
                }
            }
        }

        Console.WriteLine(sb);
    }

    public static void Main(string[] args)
    {
        if (args.Length == 0)
            throw new DirectoryNotFoundException("You didn't specified the path!");

        string libPath = args[0];

        GetMetadata(libPath);
    }
}
