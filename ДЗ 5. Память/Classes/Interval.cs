using System;

public struct Interval
{
    private static readonly Random _random = new Random();

    public float Min { get; private set; }
    public float Max { get; private set; }

    // Конструктор с проверкой входных значений
    public Interval(float minValue, float maxValue)
    {
        if (minValue > maxValue)
        {
            (minValue, maxValue) = (maxValue, minValue);
            Console.WriteLine("Некорректные входные данные: минимальное значение больше максимального, значения поменяны местами.");
        }

        if (minValue < 0)
        {
            minValue = 0;
            Console.WriteLine("Некорректное минимальное значение: установлено на 0.");
        }

        if (maxValue < 0)
        {
            maxValue = 0;
            Console.WriteLine("Некорректное максимальное значение: установлено на 0.");
        }

        if (minValue == maxValue)
        {
            maxValue += 10;
            Console.WriteLine("Минимальное и максимальное значения совпадают, максимальное значение увеличено на 10.");
        }

        Min = minValue;
        Max = maxValue;
    }

    // Метод, возвращающий случайное значение между Min и Max
    public float Get()
    {
        return (float)(_random.NextDouble() * (Max - Min) + Min);
    }
}