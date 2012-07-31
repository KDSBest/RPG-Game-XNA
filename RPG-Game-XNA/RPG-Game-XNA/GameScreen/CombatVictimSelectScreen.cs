using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGData;
using RPG_Game_XNA.Combat;

namespace RPG_Game_XNA.GameScreen
{
    public class CombatVictimSelectScreen : GameStateScreen
    {
        private int selected;
        private Character Attacker;
        private List<Character> Enemies;
        private List<Character> Group;
        private Skill Skill;

        public CombatVictimSelectScreen(Character Attacker, List<Character> Enemies, List<Character> Group, string SkillName)
            : base()
        {
            this.Attacker = Attacker;
            this.Enemies = Enemies;
            this.Group = Group;
            this.Skill = Session.currentSession.SkillPool.GetSkill(SkillName);

            selected = 0;
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            /*
            Globals.Instance.SpriteBatch.Begin();
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, FirstRect, Color.White);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.Gardient, SecondRect, SecondRectSrc, Color.Blue);
            float savedY = StringPos.Y;
            if (Skill.CastOnTeam)
            {
                for (int i = 0; i < Group.Count; i++)
                {
                    string text = Group[i].DisplayName + "\nHP: " + Group[i].HP + "/" + Group[i].MaxHP + "\nMP: " + Group[i].MP + "/" + Group[i].MaxMP;
                    if (selected == i || Skill.AoE)
                        Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, StringPos, Color.DarkRed);
                    else
                        Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, StringPos, Color.LightGray);
                    StringPos.Y += IncrementY;
                }
            }
            else
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    string text = Enemies[i].DisplayName + "\nHP: " + Enemies[i].HP + "/" + Enemies[i].MaxHP + "\nMP" + Enemies[i].MP + "/" + Enemies[i].MaxMP;
                    if (selected == i || Skill.AoE)
                        Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, StringPos, Color.DarkRed);
                    else
                        Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, StringPos, Color.LightGray);
                    StringPos.Y += IncrementY;
                }
            }
            StringPos.Y = savedY;
            Globals.Instance.SpriteBatch.End();
            */

            Point Size = new Point(500, 175);
            Globals.Instance.SpriteBatch.Begin();
            if (Skill.CastOnTeam)
            {
                int StartPosY = (int)(Globals.Instance.ScreenHeightHalf - ((float)Group.Count / 2.0f) * Size.Y);
                Point Position = new Point((int)Globals.Instance.ScreenWidthHalf - (Size.X / 2), StartPosY);
                for (int i = 0; i < Group.Count; i++)
                {
                    if (i == selected || Skill.AoE)
                        DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Wheat, Group[i]);
                    else
                        DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Gray, Group[i]);

                    Position.Y += Size.Y;
                }
            }
            else
            {
                int StartPosY = (int)(Globals.Instance.ScreenHeightHalf - ((float)Enemies.Count / 2.0f) * Size.Y);
                Point Position = new Point((int)Globals.Instance.ScreenWidthHalf - (Size.X / 2), StartPosY);
                for (int i = 0; i < Enemies.Count; i++)
                {
                    if (i == selected || Skill.AoE)
                        DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Wheat, Enemies[i]);
                    else
                        DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Gray, Enemies[i]);

                    Position.Y += Size.Y;
                }
            }
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (Skill.CastOnTeam)
            {
                if (input.IsMenuDown())
                {
                    selected++;
                    if (selected >= Group.Count)
                        selected = 0;
                }
                if (input.IsMenuUp())
                {
                    selected--;
                    if (selected < 0)
                        selected = Group.Count - 1;
                }
                if (input.IsMenuSelect())
                {
                    Attacker.MP -= Skill.ConsumeMP;
                    Attacker.ATB = 0;
                    GameStateManager.Instance.BackToCombatScreen();
                    if (Skill.AoE)
                        CombatEngine.DoDamage(Attacker, Group, Skill.Name);
                    else
                        CombatEngine.DoDamage(Attacker, Group[selected], Skill.Name);
                }
            }
            else
            {
                if (input.IsMenuDown())
                {
                    selected++;
                    if (selected >= Enemies.Count)
                        selected = 0;
                }
                if (input.IsMenuUp())
                {
                    selected--;
                    if (selected < 0)
                        selected = Enemies.Count - 1;
                }
                if (input.IsMenuSelect())
                {
                    Attacker.MP -= Skill.ConsumeMP;
                    Attacker.ATB = 0;
                    GameStateManager.Instance.BackToCombatScreen();
                    if (Skill.AoE)
                        CombatEngine.DoDamage(Attacker, Enemies, Skill.Name);
                    else
                        CombatEngine.DoDamage(Attacker, Enemies[selected], Skill.Name);
                }
            }

            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
