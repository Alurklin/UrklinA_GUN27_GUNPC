using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Weapon
    {
        public string WeaponName { get; }

        // Конструктор, принимающий имя оружия
        public Weapon(string weaponName)
        {
            WeaponName = weaponName;
        }

        public override string ToString() => WeaponName;
        public struct Interval
        {
            private int minValue;
            private int maxValue;
            private static Random random = new Random();

            public int Min => minValue;
            public int Max => maxValue;

            public Interval(int minValue, int maxValue)
            {
                // Проверка входных значений
                if (minValue < 0 || maxValue < 0)
                {
                    Console.WriteLine("Некорректные входные данные: отрицательные значения заменены на 0.");
                    this.minValue = Math.Max(0, minValue);
                    this.maxValue = Math.Max(0, maxValue);
                }
                else
                {
                    this.minValue = minValue;
                    this.maxValue = maxValue;
                }

                // Если min больше max, меняем их местами
                if (this.minValue > this.maxValue)
                {
                    Console.WriteLine("Некорректные входные данные: minValue больше maxValue. Меняем местами.");
                    int temp = this.minValue;
                    this.minValue = this.maxValue;
                    this.maxValue = temp;
                }

                // Если оба значения равны, увеличиваем maxValue на 10
                if (this.minValue == this.maxValue)
                {
                    Console.WriteLine("Некорректные входные данные: minValue и maxValue равны. Увеличиваем maxValue на 10.");
                    this.maxValue += 10;
                }
            }

            public double Get()
            {
                return random.NextDouble() * (maxValue - minValue) + minValue;
            }
        }
    }
}
