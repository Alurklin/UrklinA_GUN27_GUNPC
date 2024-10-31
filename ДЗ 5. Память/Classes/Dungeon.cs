using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Dungeon
    {
        private Room[] rooms;

        public Dungeon()
        {
            rooms = new Room[]
            {
            CreateRoom("Солдат", 20, "Шлем солдата", 5, "Броня солдата", 10, "Ботинки солдата", 3, "Ружье"),
            CreateRoom("Маг", 15, "Шлем мага", 3, "Робо мага", 5, "Ботинки мага", 2, "Посох"),
            CreateRoom("Лучник", 18, "Шлем лучника", 4, "Броня лучника", 7, "Ботинки лучника", 5, "Лук"),
            CreateRoom("Воин", 25, "Шлем воина", 6, "Броня воина", 12, "Ботинки воина", 4, "Меч"),
            CreateRoom("Разведчик", 12, "Шлем разведчика", 2, "Броня разведчика", 4, "Ботинки разведчика", 6, "Кинжал")
            };
        }

        private Room CreateRoom(string unitName, float unitHealth, string helmName, float helmArmor,
                                 string shellName, float shellArmor, string bootsName, float bootsArmor,
                                 string weaponName)
        {
            Unit unit = new Unit(unitName, unitHealth);
            unit.EquipHelm(new Helm(helmArmor, helmName));
            unit.EquipShell(new Shell(shellArmor, shellName));
            unit.EquipBoots(new Boots(bootsArmor, bootsName));
            unit.EquipWeapon(new Weapon(weaponName));

            return new Room(unit, new Weapon(weaponName));
        }

        public void ShowRooms()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                var room = rooms[i];
                Console.WriteLine("Юнит в комнате: " + room.Unit);
                Console.WriteLine("Оружие в комнате: " + room.Weapon);
                Console.WriteLine("—");
            }
        }
    }
}
