using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Weapon
    {
        public string Name { get; }
        public float MinDamage { get; set; }
        public float MaxDamage { get; set; }

        public Weapon(string name, float minDamage, float maxDamage)
        {
            Name = name;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            SetDamageParameters();
        }

        public Weapon(string name) : this(name, 1f, 10f)
        {
        }


        public float GetDamage()
        {
            return (MinDamage + MaxDamage) / 2f;
        }

        private void SetDamageParameters()
        {
            if (MinDamage > MaxDamage)
            {
                throw new ArgumentOutOfRangeException("MinDamage must be less than or equal to MaxDamage.");
            }
            if (MinDamage < 1f)
            {
                MinDamage = 1f;
                Console.WriteLine($"Minimum damage for {Name} forced to 1.");
            }
            if (MaxDamage <= 1f)
            {
                MaxDamage = 10f;
                Console.WriteLine($"Maximum damage for {Name} forced to 10.");
            }
        }
    }
}
