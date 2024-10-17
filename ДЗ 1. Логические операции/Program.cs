
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите первое число:");
        if (!Int32.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Неправильное значение первого числа!");
            return;
        }

        Console.WriteLine("Введите знак:");
        var s = Console.ReadLine();
        var boolVar = true;
        if (s.Length == 0 || s.Length > 1 && boolVar)
        {
            Console.WriteLine("Неправильный знак!");
            return;
        }

        Console.WriteLine("Введите второе число:");
        if (!Int32.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Неправильное значение второго числа!");
            return;
        }

        switch (s[0])
        {
            case '+':
                Console.WriteLine("Сумма: " + (a + b) + " {2}:" + Convert.ToString(a + b,2) + " {16}:" + Convert.ToString(a + b, 16));
                break;

            case '-':
                Console.WriteLine("Разность: " + (a - b) + " {2}:" + Convert.ToString(a - b, 2) + " {16}:" + Convert.ToString(a - b, 16));
                break;

            case '*':
                Console.WriteLine("Произведение: " + (a * b) + " {2}:" + Convert.ToString(a * b, 2) + " {16}:" + Convert.ToString(a * b, 16));
                break;

            case '/':
                Console.WriteLine("Деление: " + (a / b) + " {2}:" + Convert.ToString(a / b, 2) + " {16}:" + Convert.ToString(a / b, 16));
                break;

            case '%':
                Console.WriteLine("Остаток от деления: " + (a % b) + " {2}:" + Convert.ToString(a % b, 2) + " {16}:" + Convert.ToString(a % b, 16));
                break;

            case '&':
                Console.WriteLine("&: " + (a & b) + " {2}:" + Convert.ToString(a & b, 2) + " {16}:" + Convert.ToString(a & b, 16));
                break;

            case '|':
                Console.WriteLine("|: " + (a | b) + " {2}:" + Convert.ToString(a | b, 2) + " {16}:" + Convert.ToString(a | b, 16));
                break;

            case '^':
                Console.WriteLine("&: " + (a ^ b) + " {2}:" + Convert.ToString(a ^ b, 2) + " {16}:" + Convert.ToString(a ^ b, 16));
                break;

            default:
                Console.WriteLine("Неверный знак!");
                break;
        }
    }
}