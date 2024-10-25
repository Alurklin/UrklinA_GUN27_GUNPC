using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите номер задачи (1, 2, 3):");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                new Task1().TaskLoop();
                break;
            case "2":
                new Task2().TaskLoop();
                break;
            case "3":
                new Task3().TaskLoop();
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }

    private class Task1
    {
        public void TaskLoop()
        {
            List<string> stringList = new List<string> { "Первый", "Второй", "Третий" };

            Console.WriteLine("Список строк: " + string.Join(", ", stringList));

            Console.WriteLine("Введите новую строку для добавления в список (или '--exit' для выхода):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "--exit")
                {
                    Console.WriteLine("Завершение задачи 1.");
                    break;
                }

                stringList.Add(input);
                Console.WriteLine("Обновленный список: " + string.Join(", ", stringList));

                Console.WriteLine("Введите строку для добавления в середину списка:");
                input = Console.ReadLine();
                if (input == "--exit")
                {
                    Console.WriteLine("Завершение задачи 1.");
                    break;
                }

                stringList.Insert(stringList.Count / 2, input);
                Console.WriteLine("Обновленный список: " + string.Join(", ", stringList));
            }
        }
    }

    private class Task2
    {
        public void TaskLoop()
        {
            Dictionary<string, double> studentGrades = new Dictionary<string, double>();

            while (true)
            {
                Console.WriteLine("Введите имя студента и его оценку (или '--exit' для выхода):");
                string input = Console.ReadLine();
                if (input == "--exit")
                {
                    Console.WriteLine("Завершение задачи 2.");
                    break;
                }

                string[] parts = input.Split();
                if (parts.Length < 2 || !double.TryParse(parts[1], out double grade) || grade < 2 || grade > 5)
                {
                    Console.WriteLine("Некорректный ввод. Оценка должна быть от 2 до 5.");
                    continue;
                }

                studentGrades[parts[0]] = grade;
                Console.WriteLine("Оценка добавлена.");

                Console.WriteLine("Введите имя студента для получения его оценки:");
                string studentName = Console.ReadLine();
                if (studentGrades.TryGetValue(studentName, out double result))
                {
                    Console.WriteLine($"Оценка студента {studentName}: {result}");
                }
                else
                {
                    Console.WriteLine("Студента с таким именем не существует.");
                }
            }
        }
    }

    private class Task3
    {
        private class Node
        {
            public int Data;
            public Node Next;
            public Node Previous;
        }

        private class DoublyLinkedList
        {
            private Node head;
            private Node tail;

            public int Count { get; private set; }

            public void Add(int data)
            {
                Node newNode = new Node { Data = data };
                if (head == null)
                {
                    head = tail = newNode;
                }
                else
                {
                    tail.Next = newNode;
                    newNode.Previous = tail;
                    tail = newNode;
                }
            }

            public void PrintForward()
            {
                Node current = head;
                Console.Write("Список в прямом порядке: ");
                while (current != null)
                {
                    Console.Write(current.Data + " ");
                    current = current.Next;
                }
                Console.WriteLine();
            }

            public void PrintBackward()
            {
                Node current = tail;
                Console.Write("Список в обратном порядке: ");
                while (current != null)
                {
                    Console.Write(current.Data + " ");
                    current = current.Previous;
                }
                Console.WriteLine();
            }
        }

        public void TaskLoop()
        {
            DoublyLinkedList list = new DoublyLinkedList();
            Console.WriteLine("Введите от 3 до 6 элементов (или '--exit' для выхода):");

            while (list.Count < 6)
            {
                string input = Console.ReadLine();
                if (input == "--exit")
                {
                    Console.WriteLine("Завершение задачи 3.");
                    return;
                }

                if (int.TryParse(input, out int number))
                {
                    list.Add(number);
                    Console.WriteLine($"Элемент {number} добавлен.");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                }
            }

            list.PrintForward();
            list.PrintBackward();
        }
    }
}
