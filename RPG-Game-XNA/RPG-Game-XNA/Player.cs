using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;

namespace RPG_Game_XNA
{
    public class Player
    {
        public int Vitality
        {
            get
            {
                return 0;
            }
        }
        public int Strength
        {
            get
            {
                return 0;
            }
        }

        public int Magic
        {
            get
            {
                return 0;
            }
        }

        public int Spirit
        {
            get
            {
                return 0;
            }
        }

        public int Luck
        {
            get
            {
                return 0;
            }
        }

        public int HP
        {
            get
            {
                return 0;
            }
        }

        public int MP
        {
            get
            {
                return 0;
            }
        }

        public int Dexterity
        {
            get
            {
                return 0;
            }
        }

        public int Attack
        {
            get
            {
                return (Strength + Weapon.Damage) * Weapon.DamagePercentBonus;
            }
        }

        public int Defense
        {
            get
            {
                return (Vitality + Armour.Defense) * Armour.DefensePercentBonus;
            }
        }

        public int MagicAttack
        {
            get
            {
                return (Magic + Weapon.MagicDamage) * Weapon.MagicDamagePercentBonus;;
            }
        }

        public int MagicDefense
        {
            get
            {
                return (Spirit + Armour.MagicDefense) * Armour.MagicDefensePercentBonus;
            }
        }

        public Weapon Weapon;
        public Armour Armour;

        public int Level;
        public int Experience;

        public int ExperienceForNextLevel()
        {
            int L = Level + 1;
            int Exp = 0;
            float K;
            if (Level <= 10)
                K = 7;
            else if (Level <= 20)
                K = 7.3f;
            else if (Level <= 30)
                K = 7.5f;
            else if (Level <= 40)
                K = 7.6f;
            else
                K = 7.7f;
            for (int a = 1; a < L; a++)
                Exp += (int) (K * a * a);
            return Exp;
        }
    }
}
