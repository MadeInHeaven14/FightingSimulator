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

    }
}
