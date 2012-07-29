using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;

namespace RPG_Game_XNA
{
    public class Character
    {
        public Weapon Weapon;
        public Armour Armour;
        public CharacterData CharInfo;
        public List<Skill> Skills;

        public int Level;
        private int _Experience;
        public int Experience
        {
            get
            {
                return _Experience;
            }
            set
            {
                _Experience = value;
                while (ExperienceForNextLevel() < _Experience && Level < MaxLevel)
                    Level++;
            }
        }
        public string Name;
        public int ATB;

        public List<ScriptEngineCommand> AI;
        public const int MaxLevel = 99;

        public Character(string Name, Weapon Weapon, Armour Armour)
            : this(Name, 0, Weapon, Armour)
        {

        }

        public Character(string Name, int Experience, Weapon Weapon, Armour Armour)
        {
            this.Name = Name;
            this.DisplayName = Name;
            CharInfo = Globals.Instance.Content.Load<CharacterData>("Character\\" + Name);

            Level = 1;
            this.Experience = Experience;
            this.Weapon = Weapon;
            this.Armour = Armour;
            this.ATB = 0;
            HP = MaxHP;
            MP = MaxMP;
            Skills = new List<Skill>();
        }

        public void SetLevel(int Level)
        {
            this.Level = Level;
            this.HP = MaxHP;
            this.MP = MaxMP;
        }

        public static int BasicStatFormula(int Base, int Max, int X, int MaxX)
        {
            float val = (float)X / (float)MaxX;
            return (int)(Base + ((float)(Max - Base)) * ((val * val + val) / 2));
        }

        public int HP;
        public int MP;

        public int Vitality
        {
            get
            {
                return BasicStatFormula(CharInfo.VitalityBase, CharInfo.VitalityMax, Level, MaxLevel);
            }
        }
        public int Strength
        {
            get
            {
                return BasicStatFormula(CharInfo.StrengthBase, CharInfo.StrengthMax, Level, MaxLevel);
            }
        }

        public int Magic
        {
            get
            {
                return BasicStatFormula(CharInfo.MagicBase, CharInfo.MagicMax, Level, MaxLevel);
            }
        }

        public int Spirit
        {
            get
            {
                return BasicStatFormula(CharInfo.SpiritBase, CharInfo.SpiritMax, Level, MaxLevel);
            }
        }

        public int Luck
        {
            get
            {
                return BasicStatFormula(CharInfo.LuckBase, CharInfo.LuckMax, Level, MaxLevel);
            }
        }

        public int Dexterity
        {
            get
            {
                return BasicStatFormula(CharInfo.DexterityBase, CharInfo.DexterityMax, Level, MaxLevel);
            }
        }

        public int MaxHP
        {
            get
            {
                return BasicStatFormula(CharInfo.HPBase, CharInfo.HPMax, Vitality, 255);
            }
        }

        public int MaxMP
        {
            get
            {
                return BasicStatFormula(CharInfo.MPBase, CharInfo.MPMax, Spirit, 255);
            }
        }

        public int Attack
        {
            get
            {
                return (Strength + Weapon.Damage);
            }
        }

        public int Defense
        {
            get
            {
                return (Vitality + Armour.Defense);
            }
        }

        public int MagicAttack
        {
            get
            {
                return (Magic + Weapon.MagicDamage);
            }
        }

        public int MagicDefense
        {
            get
            {
                return (Spirit + Armour.MagicDefense);
            }
        }

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

        public void Damage(int Damage)
        {
            this.HP -= Damage;
            if (this.HP < 0)
                this.HP = 0;
            if (this.HP > this.MaxHP)
                this.HP = this.MaxHP;
        }

        public string DisplayName;
        public bool IsAlive 
        {
            get
            {
                return HP > 0;
            }
        }
    }
}
