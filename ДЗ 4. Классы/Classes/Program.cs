using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Подготовка к бою:");

            string playerName = "";
            float initialHealth = 0;
            float helmetArmor = 0;
            float shellArmor = 0;
            float bootArmor = 0;
            float minimumWeaponDamage = 0;
            float maximumWeaponDamage = 0;

            Console.WriteLine("Введите имя бойца:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите начальное здоровье бойца (10-100):");
            while (!float.TryParse(Console.ReadLine(), out initialHealth) || initialHealth < 10 || initialHealth > 100)
            {
                Console.WriteLine("Неверное значение здоровья. Попробуйте снова.");
            }

            Console.WriteLine("Введите значение брони шлема от 0, до 1:");
            while (!float.TryParse(Console.ReadLine(), out helmetArmor) || helmetArmor < 0 || helmetArmor > 1)
            {
                Console.WriteLine("Неверное значение брони шлема. Попробуйте снова.");
            }

            Console.WriteLine("Введите значение брони кирасы от 0, до 1:");
            while (!float.TryParse(Console.ReadLine(), out shellArmor) || shellArmor < 0 || shellArmor > 1)
            {
                Console.WriteLine("Неверное значение брони кирасы. Попробуйте снова.");
            }

            Console.WriteLine("Введите значение брони сапог от 0, до 1:");
            while (!float.TryParse(Console.ReadLine(), out bootArmor) || bootArmor < 0 || bootArmor > 1)
            {
                Console.WriteLine("Неверное значение брони сапог. Попробуйте снова.");
            }

            Console.WriteLine("Укажите минимальный урон оружия (0-20): ");
            while (!float.TryParse(Console.ReadLine(), out minimumWeaponDamage) || minimumWeaponDamage < 0 || minimumWeaponDamage > 20)
            {
                Console.WriteLine("Неверное значение минимального урона оружия. Попробуйте снова.");
            }

            Console.WriteLine("Укажите максимальный урон оружия (20-40): ");
            while (!float.TryParse(Console.ReadLine(), out maximumWeaponDamage) || maximumWeaponDamage < 20 || maximumWeaponDamage > 40)
            {
                Console.WriteLine("Неверное значение максимального урона оружия. Попробуйте снова.");
            }

            Unit playerUnit = new Unit(playerName, initialHealth, helmetArmor + shellArmor + bootArmor);

            Console.WriteLine("Общий показатель брони равен: " + playerUnit.Armor);
            Console.WriteLine("Фактическое значение здоровья равно: " + playerUnit.RealHealth);
        }


    }
}
