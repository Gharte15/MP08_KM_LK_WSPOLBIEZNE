using Calculator;

Console.WriteLine("[1] Dodawanie");
Console.WriteLine("[2] Odejmowanie");
Console.WriteLine("[3] Mnozenie");
Console.WriteLine("[4] Dzielenie");
Console.Write("Wybor: ");

int operation = Convert.ToInt32(Console.ReadLine());

Console.Write("Podaj pierwsza liczbe: ");
int a = Convert.ToInt32(Console.ReadLine());

Console.Write("Podaj druga liczbe: ");
int b = Convert.ToInt32(Console.ReadLine());

CalculatorOperations operations = new CalculatorOperations();

switch (operation)
{
    case 1:
        Console.WriteLine("a + b = " + operations.Add(a, b));
        break;
    case 2:
        Console.WriteLine("a - b = " + operations.Substract(a, b));
        break;
    case 3:
        Console.WriteLine("a * b = " + operations.Multiply(a, b));
        break;
    case 4:
        Console.WriteLine("a / b = " + operations.Divide(a, b));
        break;
    default:
        Console.WriteLine("Nieznana operacja");
        break;
}