using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting
{
    public class Fighter
    {
        public string Name { get; set; }
        public string Attack { get; set; }
        public string Block { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Luck { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public double PhAttack { get; set; }
        public double BlockBreak { get; set; }
        public double Evasion { get; set; }
        public double CrtChance { get; set; }
        public double HP { get; set;  }
        public double MaxHP { get; set; }
        public double MageAttack { get; set; }
        public double Mana { get; set; }
        public int Lvl { get; set; }
        public double Exp { get; set; }
        public int Point { get; set; }

        public Fighter(string name, int strength, int dexterity, int luck, int constitution, int intelligence)
        {
            Name = name;
            Strength = strength;
            Dexterity = dexterity;
            Luck = luck;
            Constitution = constitution;
            Intelligence = intelligence;
            PhAttack = 1 * Strength;
            HP = 5 * Constitution;
            MaxHP = HP;
            MageAttack = 2 * Intelligence;
            Mana = 7 * Intelligence;
            Lvl = 1;
            Exp = 0;
            Point = 15;
        }
    }
}
