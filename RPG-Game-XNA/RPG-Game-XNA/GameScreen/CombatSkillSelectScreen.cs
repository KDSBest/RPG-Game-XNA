using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGData;

namespace RPG_Game_XNA.GameScreen
{
    public class CombatSkillSelectScreen : GameStateScreen
    {
        private int selected;
        private Rectangle FirstRect, SecondRect, SecondRectSrc;
        private Vector2 StringPos;
        private int IncrementY;
        private Character Attacker;
        private List<Character> Enemies;
        private List<Character> Group;

        public CombatSkillSelectScreen(Character Attacker, List<Character> Enemies, List<Character> Group)
            : base()
        {
            this.Attacker = Attacker;
            this.Enemies = Enemies;
            this.Group = Group;

            selected = 0;

            int posX = 300;
            int posY = 100;
            int w = 300;
            int h = 400;
            FirstRect = new Rectangle(posX, posY, w, h);
            SecondRect = new Rectangle(posX + 2, posY + 2, w - 4, h - 4);
            SecondRectSrc = new Rectangle(700, 800, 300, 200);
            StringPos = new Vector2(posX + 15, posY + 15);
            IncrementY = (int) Globals.Instance.SpriteFont.MeasureString("A").Y + 10;
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Globals.Instance.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, FirstRect, Color.White);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.Gardient, SecondRect, SecondRectSrc, Color.Blue);
            for (int i = 0; i < Attacker.Skills.Count; i++)
            {
                if(selected == i)
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Attacker.Skills[i].Name, StringPos, Color.DarkRed);
                else
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Attacker.Skills[i].Name, StringPos, Color.LightGray);
                StringPos.Y += IncrementY;
            }
            StringPos.Y -= Attacker.Skills.Count * IncrementY;
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Attacker.Skills.Count)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Attacker.Skills.Count - 1;
            }

            if (input.IsMenuSelect())
            {
                if(Attacker.MP >= Attacker.Skills[selected].ConsumeMP)
                    GameStateManager.Instance.AddScreen(new CombatVictimSelectScreen(Attacker, Enemies, Group, Attacker.Skills[selected].Name), true, false);
            }

            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
