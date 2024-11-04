using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Unit
    {
        private float _health;
        private float _armor;

        private Helm _helm;
        private Shell _shell;
        private Boots _boots;
        private Weapon _weapon;

        public string Name { get; }

        public float Health => _health;

        public Unit() : this(name: "Unknown unit", 10)
        {
        }

        public Unit(string name, float health)
        {
            Name = name;
            _health = health;
        }

        public float RealHealth()
        {
            return Health * (1f + Armor);
        }

        public float Armor
        {
            get { return (float)Math.Round(_armor, 2); }
            set
            {
                if (value >= 0 && value <= 1)
                {
                    _armor = value;
                }
            }
        }

        public bool SetDamage(float value)
        {
            if (_weapon == null)
            {
                Console.WriteLine("Оружие не экипировано, урон не нанесен.");
                return false;
            }

            float damage = _weapon.DamageRange.Get(); // Получаем случайный урон в диапазоне оружия
            _health -= damage * (1f - Armor); // Уменьшаем здоровье с учетом брони
            Console.WriteLine($"{Name} получил {damage * (1f - Armor):F2} урона. Оставшееся здоровье: {_health:F2}");

            return _health <= 0f;
        }

        public void EquipHelm(Helm helm)
        {
            _helm = helm;
        }
        public void EquipShell(Shell shell)
        {
            _shell = shell;
        }
        public void EquipBoots(Boots boots)
        {
            _boots = boots;
        }

        public void EquipWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public struct Room
    {
        public Unit Unit { get; }
        public Weapon Weapon { get; }

        public Room(Unit unit, Weapon weapon)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit), "Юнит не может быть null");
            Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon), "Оружие не может быть null");
        }
    }
}
