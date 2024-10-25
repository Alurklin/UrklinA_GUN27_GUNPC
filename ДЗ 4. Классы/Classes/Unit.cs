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

        public Unit() : this(name: "Unknow unit", 10)
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
                if (value >= 0 || value <= 1)
                {
                    _armor = value;
                }
                else { }
            }
        }

        public bool SetDamage(float value)
        {
            _health -= value * Armor;
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

    }
}
