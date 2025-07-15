using task14;

namespace task14tests;

public class DefiniteIntegralTests
{
    Func<double, double> X = (double x) => x;

    Func<double, double> SIN = Math.Sin;

    [Fact]
    public void DefiniteIntegral_Solve_XReturnsZero() => Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2), 1e-5);

    [Fact]
    public void DefiniteIntegral_Solve_SinReturnsZero() => Assert.Equal(0, DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8), 1e-4);

    [Fact]
    public void DefiniteIntegral_Solve_XReturnsTwelvePointFive() => Assert.Equal(12.5, DefiniteIntegral.Solve(0, 5, X, 1e-6, 8), 1e-5);
}
