using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting
{
    public class FighterService
    {
        public static List<Fighter> FighterList = new List<Fighter>();

        public static string Name1;
        public static int S1;
        public static int D1;
        public static int L1;
        public static int C1;
        public static int I1;
        public static double PhAt1;
        public static double Bb1;
        public static double Ev1;
        public static double CrCh1;
        public static double HP1;
        public static double MagAt1;
        public static double Mana1;
        public static int Lvl1;
        public static double Exp1;
        public static int P1;

        public static string Name2;
        public static int S2;
        public static int D2;
        public static int L2;
        public static int C2;
        public static int I2;
        public static double PhAt2;
        public static double Ev2;
        public static double CrCh2;
        public static double HP2;
        public static double MagAt2;
        public static double Mana2;
        public static int Lvl2;
        public static double Exp2;
        public static int P2;

        public static void CreateFirstPlayer(Fighter unit)
        {
            S1 = unit.Strength;
            D1 = unit.Dexterity;
            L1 = unit.Luck;
            C1 = unit.Constitution;
            I1 = unit.Intelligence;
            PhAt1 = unit.PhAttack;
            Bb1 = unit.BlockBreak;
            Ev1 = unit.Evasion;
            CrCh1 = unit.CrtChance;
            HP1 = unit.HP;
            MagAt1 = unit.MageAttack;
            Mana1 = unit.Mana;
            Lvl1 = unit.Lvl;
            Exp1 = unit.Exp;
            P1 = unit.Point;
            FighterList.Add(unit);
            return;
        }

        public static void CreateSecondPlayer(Fighter unit)
        {
            S2 = unit.Strength;
            D2 = unit.Dexterity;
            L2 = unit.Luck;
            C2 = unit.Constitution;
            I2 = unit.Intelligence;
            PhAt2 = unit.PhAttack;
            Ev2 = unit.Evasion;
            CrCh2 = unit.CrtChance;
            HP2 = unit.HP;
            MagAt2 = unit.MageAttack;
            Mana2 = unit.Mana;
            Lvl2 = unit.Lvl;
            Exp2 = unit.Exp;
            P2 = unit.Point;
            FighterList.Add(unit);
            return;
        }

        public static void LoadFighter1(Fighter unit)
        {
            Name1 = unit.Name;
            S1 = unit.Strength;
            D1 = unit.Dexterity;
            L1 = unit.Luck;
            C1 = unit.Constitution;
            I1 = unit.Intelligence;
            PhAt1 = unit.PhAttack;
            Bb1 = unit.BlockBreak;
            Ev1 = unit.Evasion;
            CrCh1 = unit.CrtChance;
            HP1 = unit.HP;
            MagAt1 = unit.MageAttack;
            Mana1 = unit.Mana;
            Lvl1 = unit.Lvl;
            Exp1 = unit.Exp;
            P1 = unit.Point;
            return;
        }

        public static void LoadFighter2(Fighter unit)
        {
            Name2 = unit.Name;
            S2 = unit.Strength;
            D2 = unit.Dexterity;
            L2 = unit.Luck;
            C2 = unit.Constitution;
            I2 = unit.Intelligence;
            PhAt2 = unit.PhAttack;
            Ev2 = unit.Evasion;
            CrCh2 = unit.CrtChance;
            HP2 = unit.HP;
            MagAt2 = unit.MageAttack;
            Mana2 = unit.Mana;
            Lvl2 = unit.Lvl;
            Exp2 = unit.Exp;
            P2 = unit.Point;
            return;
        }

    }
}
