namespace task05;

using System;
using System.Reflection;
using System.Collections.Generic;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
        => _type.GetMethods().Where(x => x.IsPublic).Select(x => x.Name);

    public IEnumerable<string?> GetMethodParams(string methodName)
    {
        var method = _type.GetMethod(methodName);
        
        if (method == null)
        {
            return Enumerable.Empty<string>();
        }

        return method.GetParameters().Select(x => x.Name);
    }

    public IEnumerable<string> GetAllFields()
        => _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(x => x.Name);

    public IEnumerable<string> GetProperties()
        => _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Select(x => x.Name);

    public bool HasAttribute<T>() where T : Attribute
        => _type.GetCustomAttributes<T>().Any();
}
