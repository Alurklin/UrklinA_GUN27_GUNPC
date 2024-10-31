using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Helm
    {
        public string Name { get; }
        public float Armor { get; private set; }

        public Helm(string name = "Helm")
        {
            Name = name;
        }

        public Helm(float armor, string name = "Helm") : this(name)
        {
            Armor = armor;
        }
    }

    public class Shell
    {
        public string Name { get; }
        public float Armor { get; private set; }

        public Shell(string name = "Shell")
        {
            Name = name;
        }

        public Shell(float armor, string name = "Shell") : this(name)
        {
            Armor = armor;
        }
    }

    public class Boots
    {
        public string Name { get; }
        public float Armor { get; private set; }

        public Boots(string name = "Boots")
        {
            Name = name;
        }

        public Boots(float armor, string name = "Boots") : this(name)
        {
            Armor = armor;
        }
    }
}
