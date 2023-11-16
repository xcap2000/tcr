namespace Tcr;

public class CalculatorTests
{
    [Fact]
    public void Sum_returns_2()
    {
        // Given
        var calculator = new Calculator();

        // When
        var result = calculator.Sum(1, 1);

        // Then
        Assert.Equal(2, result);
    }

    [Fact]
    public void Sum_returns_3()
    {
        // Given
        var calculator = new Calculator();

        // When
        var result = calculator.Sum(1, 2);

        // Then
        Assert.Equal(3, result);
    }
}