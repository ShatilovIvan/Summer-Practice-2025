using task13;

namespace task13tests;

public class StudentSerializerTests
{
    [Fact]
    public void StudentSerializerTests_SerializeReturnsValidJson()
    {
        Student student = new Student("Mark", "Zukerberg", new DateTime(2000, 1, 1), new List<Subject>
        {
            new Subject("Math", 90),
            new Subject("Science", 85)
        });

        var serializer = new StudentSerializer("dd-MM-yyyy");

        serializer.Serialize(student, "student.json");

        string expectedJSON = "{\n  \"FirstName\": \"Mark\",\n  \"LastName\": \"Zukerberg\",\n  \"BirthDate\": \"01-01-2000\",\n  \"Grades\": [\n    {\n      \"Name\": \"Math\",\n      \"Grade\": 90\n    },\n    {\n      \"Name\": \"Science\",\n      \"Grade\": 85\n    }\n  ]\n}";

        string actualJSON = File.ReadAllText("student.json");

        Assert.Equal(expectedJSON, actualJSON);

        File.Delete("student.json");
    }

    [Fact]
    public void StudentSerializerTests_DeserializeReturnsValidStudent()
    {
        string json = "{\n  \"FirstName\": \"Mark\",\n  \"LastName\": \"Zukerberg\",\n  \"BirthDate\": \"01-01-2000\",\n  \"Grades\": [\n    {\n      \"Name\": \"Math\",\n      \"Grade\": 90\n    },\n    {\n      \"Name\": \"Science\",\n      \"Grade\": 85\n    }\n  ]\n}";

        File.WriteAllText("student.json", json);

        var serializer = new StudentSerializer("dd-MM-yyyy");

        Student student = serializer.Deserialize("student.json");

        Assert.Equal("Mark", student.FirstName);
        Assert.Equal("Zukerberg", student.LastName);
        Assert.Equal(new DateTime(2000, 1, 1), student.BirthDate);
        Assert.Equal(2, student.Grades.Count);
        Assert.Equal("Math", student.Grades[0].Name);
        Assert.Equal(90, student.Grades[0].Grade);
        Assert.Equal("Science", student.Grades[1].Name);
        Assert.Equal(85, student.Grades[1].Grade);

        File.Delete("student.json");
    }

    [Fact]
    public void StudentSerializerTests_SerializeThrowsArgumentNullExceptionForNullStudent()
    {
        var serializer = new StudentSerializer("dd-MM-yyyy");

        Assert.Throws<ArgumentNullException>(() => serializer.Serialize(null!, "student.json"));
    }

    [Fact]
    public void StudentSerializerTests_SerializeThrowsArgumentNullExceptionForNullFilePath()
    {
        var serializer = new StudentSerializer("dd-MM-yyyy");

        Assert.Throws<ArgumentNullException>(() => serializer.Serialize(new Student("Mark", "Zukerberg", new DateTime(2000, 1, 1), new List<Subject>()), null!));
    }

    [Fact]
    public void StudentSerializerTests_DeserializeThrowsArgumentNullExceptionForNullFilePath()
    {
        var serializer = new StudentSerializer("dd-MM-yyyy");

        Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null!));
    }

    [Fact]
    public void StudentSerializerTests_DeserializeThrowsArgumentExceptionForEmptyFile()
    {
        File.WriteAllText("empty.json", string.Empty);

        var serializer = new StudentSerializer("dd-MM-yyyy");

        Assert.Throws<ArgumentException>(() => serializer.Deserialize("empty.json"));

        File.Delete("empty.json");
    }
}
