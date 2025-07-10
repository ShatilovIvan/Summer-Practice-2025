using task11;

namespace task11tests;

public class ClassGeneratorTests
{
    [Fact]
    public void ClassGenerator_CalculatorAddReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.Generate();

        int result = calculator.Add(2, 5);

        Assert.Equal(7, result);
    }

    [Fact]
    public void ClassGenerator_CalculatorMinusReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.Generate();

        int result = calculator.Minus(5, 2);

        Assert.Equal(3, result);
    }

    [Fact]
    public void ClassGenerator_CalculatorMulReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.Generate();

        int result = calculator.Mul(5, 2);

        Assert.Equal(10, result);
    }

    [Fact]
    public void ClassGenerator_CalculatorDivReturnsCorrectValue()
    {
        ICalculator calculator = ClassGenerator.Generate();

        int result = calculator.Div(9, 3);

        Assert.Equal(3, result);
    }

    [Fact]
    public void ClassGenerator_CreatesCorrectObject()
    {
        object calculator = ClassGenerator.Generate();

        Assert.IsAssignableFrom<ICalculator>(calculator);
    }
}
