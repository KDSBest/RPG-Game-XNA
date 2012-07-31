using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGData;
using Microsoft.Xna.Framework;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;

namespace RPG_Game_XNA.Combat
{
    public static class CombatEngine
    {
        public static int MAXATB = 300;

        public static EnemyGroup CreateRandomForMap(Map map)
        {
            int MaxRandom = 0;
            foreach (EnemyGroup group in map.Enemies)
                MaxRandom += group.Random;
            int Random = Globals.Instance.Random.Next(MaxRandom);
            foreach (EnemyGroup group in map.Enemies)
            {
                Random -= group.Random;
                if (Random <= 0)
                {
                    return group;
                }
            }
            return null;
        }

        public static int CalcATB(Character Char, GameTime time)
        {
            float Dex = (float)Char.Dexterity / 255;
            float ATB = Dex * (float)time.ElapsedGameTime.TotalMilliseconds;
            int Random = Globals.Instance.Random.Next(255);
            ATB = (int)(ATB * Random / 255.0f);

            Char.ATB += (int) ATB;
            if (Char.ATB > MAXATB)
                Char.ATB = MAXATB;
            return Char.ATB;
        }

        public static int CalcSkill(Character Attacker, Character Victim, Skill Skill)
        {
            float BaseDamage = 0;
            float Power = 1;
            if (Skill != null)
            {
                switch (Skill.DamageType)
                {
                    // 0 Physical
                    case 0:
                        BaseDamage = Attacker.Attack;
                        BaseDamage += ((float)(BaseDamage + Attacker.Level) / 32.0f) * ((float)(BaseDamage * Attacker.Level) / 32.0f);
                        BaseDamage -= Victim.Defense;
                        Power = Skill.Power;
                        if (BaseDamage <= 0)
                            BaseDamage = 1;
                        break;
                    // 1 Magical
                    case 1:
                        BaseDamage = Attacker.Magic;
                        BaseDamage += ((float)(BaseDamage + Attacker.Level) / 32.0f) * ((float)(BaseDamage * Attacker.Level) / 32.0f);
                        BaseDamage -= Victim.MagicDefense;
                        Power = Skill.Power;
                        if (BaseDamage <= 0)
                            BaseDamage = 1;
                        break;
                    // 2 Heal
                    case 2:
                        BaseDamage = Attacker.Magic;
                        BaseDamage += ((float)(BaseDamage + Attacker.Level) / 32.0f) * ((float)(BaseDamage * Attacker.Level) / 32.0f);
                        BaseDamage *= -1;
                        Power = Skill.Power;
                        break;
                }
            }
            else
                BaseDamage = Attacker.Attack - Victim.Defense;
            float MaxDamage = BaseDamage * Power;
            int Random = Globals.Instance.Random.Next(255);
            int Damage = (int)(MaxDamage * (3841.0f + Random) / 4096.0f);

            int Luck = Attacker.Luck - Victim.Luck;
            if (Luck >= 100)
            {
                Damage *= 2;
            }
            else if (Luck > 0)
            {
                Random = Globals.Instance.Random.Next(100);
                if (Random <= Luck)
                    Damage *= 2;
            }
            return Damage;
        }

        public static void ShowDamage(Character Attacker, Character Victim, string Skill, int Damage)
        {
            string[] Text = new string[1];
            if(Damage >= 0)
                Text[0] = Attacker.DisplayName + " uses " + Skill + " on " + Victim.DisplayName + ".\n" + Victim.DisplayName + " gets " + Damage + " Damage.";
            else
                Text[0] = Attacker.DisplayName + " uses " + Skill + " on " + Victim.DisplayName + ".\n" + Victim.DisplayName + " gets " + (-1*Damage) + " HP Healed.";

            GameStateManager.Instance.AddScreen(new PopUpScreen(Text), true, false);
        }

        public static void DoDamage(Character Attacker, Character Victim, string Skill)
        {
            int Damage = CombatEngine.CalcSkill(Attacker, Victim, Session.currentSession.SkillPool.GetSkill(Skill));
            Victim.Damage(Damage);
            ShowDamage(Attacker, Victim, Skill, Damage);
        }

        public static void DoDamage(Character Attacker, List<Character> Victims, string Skill)
        {
            foreach (Character c in Victims)
            {
                DoDamage(Attacker, c, Skill);
            }
        }
    }
}
