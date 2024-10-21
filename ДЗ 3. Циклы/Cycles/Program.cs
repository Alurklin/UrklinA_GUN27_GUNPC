namespace HomeWork
{
    internal class Program
    {
        private const int MAX_FIBONACCI_NUMBERS = 10;

        static void Main(string[] args)
        {
            // Задание 1: Числа Фибоначчи
            int currentNumber = 0;
            int previousNumber = 1;
            int nextNumber;

            for (int i = 0; i < MAX_FIBONACCI_NUMBERS; i++)
            {
                nextNumber = currentNumber + previousNumber;
                currentNumber = previousNumber;
                previousNumber = nextNumber;
                Console.WriteLine(nextNumber);
            }

            // Задание 2: Чётные числа от 2 до 20
            for (int i = 2; i <= 20; i += 2)
            {
                Console.WriteLine(i);
            }

            // Задание 3: Таблица умножения от 1 до 5
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    Console.WriteLine($"{i} * {j} = {i * j}");
                }
                Console.WriteLine(); // Переход на новую строку
            }

            // Задание 4: Ввод пароля
            string correctPassword = "qwerty";
            string enteredPassword;

            do
            {
                Console.Write("Введите пароль: ");
                enteredPassword = Console.ReadLine();
            } while (enteredPassword != correctPassword);

            Console.WriteLine("Пароль введен правильно!");
        }
    }
}