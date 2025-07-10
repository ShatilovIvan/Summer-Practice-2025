using task09;

namespace task09tests;

public class MetadataCollectorTests
{
	[Fact]
	public void MetadataCollector_PrintsCorrectInfo()
	{
		string? baseDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;

		if (baseDir is null)
			throw new DirectoryNotFoundException("Couldn't find a directory!");

		var sw = new StringWriter();
		Console.SetOut(sw);

		string libPath = Path.Combine(new string[] { baseDir, "task09", "bin", "Debug", "net9.0", "task07.dll" });

		string expected = "Class: DisplayNameAttribute\n\tAttributes: \n\t\tSystem.Runtime.CompilerServices.NullableContextAttribute\n\t\tSystem.Runtime.CompilerServices.NullableAttribute\n\t\tSystem.AttributeUsageAttribute\n\tMethods: \n\t\tget_DisplayName\n\tConstructors: \n\t\t.ctor\n\t\t\tparameter: displayName, type: System.Reflection.RuntimeParameterInfo\nClass: ReflectionHelper\n\tMethods: \n\t\tPrintTypeInfo\n\t\t\tparameter: type, type: System.Reflection.RuntimeParameterInfo\nClass: VersionAttribute\n\tAttributes: \n\t\tSystem.AttributeUsageAttribute\n\tMethods: \n\t\tget_Major\n\t\tget_Minor\n\tConstructors: \n\t\t.ctor\n\t\t\tparameter: major, type: System.Reflection.RuntimeParameterInfo\n\t\t\tparameter: minor, type: System.Reflection.RuntimeParameterInfo\n\n";

		MetadataCollector.GetMetadata(libPath);

		Assert.Contains(expected, sw.ToString());
	}

	[Fact]
	public void MetadataCollector_ThrowsExceptionWithEmptyPath()
	{
		string[] args = [];
		
		Assert.Throws<DirectoryNotFoundException>(() => MetadataCollector.Main(args));
	}
}
