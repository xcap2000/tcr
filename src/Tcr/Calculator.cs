namespace Tcr;

public class Calculator
{
    public int Sum(int number1, int number2)
    {
        return checked(number1 + number2);
    }
}