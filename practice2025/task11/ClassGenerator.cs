using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11;

public interface ICalculator
{
    public int Add(int a, int b);
    public int Minus(int a, int b);
    public int Mul(int a, int b);
    public int Div(int a, int b);
}
public class ClassGenerator
{
    public static ICalculator Generate()
    {
        string calculatorSource = @"public class Calculator : task11.ICalculator
        {
            public int Add(int a, int b) => a + b;
            public int Minus(int a, int b) => a - b;
            public int Mul(int a, int b) => a * b;
            public int Div(int a, int b) => a / b;
        }";

        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(calculatorSource);

        var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create("DynamicCalculator", new[] { syntaxTree }, references, compilationOptions);

        using var stream = new MemoryStream();
        compilation.Emit(stream);

        Assembly assembly = Assembly.Load(stream.ToArray());

        Type calculatorType = assembly.GetType("Calculator");
        ICalculator typeInstance = Activator.CreateInstance(calculatorType) as ICalculator;

        if (typeInstance is null)
            throw new ArgumentNullException("Couldn't generate a class!");

        return typeInstance;
    }
}
