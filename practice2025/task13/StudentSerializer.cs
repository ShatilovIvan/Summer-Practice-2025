using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class StudentSerializer
{
    private JsonSerializerOptions _options;

    public StudentSerializer(string format)
    {
        _options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
        };

        _options.Converters.Add(new DateTimeConverter(format));
    }

    public void Serialize(Student student, string filePath)
    {
        if (filePath is null)
        {
            throw new ArgumentNullException("File path cannot be null.");
        }

        if (student is null)
        {
            throw new ArgumentNullException("Student cannot be null.");
        }

        string jsonString = JsonSerializer.Serialize(student, _options);
        File.WriteAllText(filePath, jsonString);
    }

    public Student Deserialize(string filePath)
    {
        if (filePath is null)
        {
            throw new ArgumentNullException("File path cannot be null.");
        }

        string jsonString = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            throw new ArgumentException("File is empty");
        }

        var student = JsonSerializer.Deserialize<Student>(jsonString, _options);

        if (student is null)
        {
            throw new InvalidOperationException("Deserialization returned null.");
        }

        return student;
    }
}
