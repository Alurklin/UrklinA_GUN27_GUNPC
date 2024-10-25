using System;
using System.Text;

class Program
{
    static void Main()
    {
        // Задание 1
        Console.WriteLine(ConcatenateStrings("Hello, ", "world!"));

        // Задание 2
        Console.WriteLine(GreetUser("Alice", 30));

        // Задание 3
        Console.WriteLine(AnalyzeString("Hello, world!"));

        // Задание 4
        Console.WriteLine(GetFirstFiveCharacters("Hello, world!"));

        // Задание 5
        string[] phrases = { "This", "is", "a", "test." };
        Console.WriteLine(BuildString(phrases).ToString());

        // Задание 6
        Console.WriteLine(ReplaceWord("Hello world, world!", "world", "C#"));
    }

    // Задание 1
    static string ConcatenateStrings(string str1, string str2)
    {
        return str1 + str2;
    }

    // Задание 2
    static string GreetUser(string name, int age)
    {
        return $"Hello, {name}! You are {age} years old.\n";
    }

    // Задание 3
    static string AnalyzeString(string str)
    {
        int length = str.Length;
        string upper = str.ToUpper();
        string lower = str.ToLower();
        return $"Length: {length}, Upper: {upper}, Lower: {lower}";
    }

    // Задание 4
    static string GetFirstFiveCharacters(string str)
    {
        return str.Substring(0, Math.Min(5, str.Length)); // Обработка случая, когда строка короче 5 символов
    }

    // Задание 5
    static StringBuilder BuildString(string[] phrases)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string phrase in phrases)
        {
            sb.Append(phrase + " "); // Используем Append
        }
        return sb;
    }

    // Задание 6
    static string ReplaceWord(string input, string wordToFind, string wordToReplace)
    {
        return input.Replace(wordToFind, wordToReplace);
    }
}