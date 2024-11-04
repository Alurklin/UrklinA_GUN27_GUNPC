using System;

public class Weapon
    {
        public string Name { get; }
        public Interval DamageRange { get; private set; }
        public float Damage => (DamageRange.Min + DamageRange.Max) / 2;

        // Конструктор, принимающий имя оружия и параметры урона
        public Weapon(string name, float minDamage, float maxDamage)
        {
            Name = name;
            DamageRange = new Interval(minDamage, maxDamage);
        }

        // Конструктор без имени
        public Weapon(float minDamage, float maxDamage) : this("Unnamed weapon", minDamage, maxDamage)
        {
        }

        // Метод для изменения диапазона урона
        public void SetDamageParams(float minDamage, float maxDamage)
        {
            DamageRange = new Interval(minDamage, maxDamage);
        }
    }