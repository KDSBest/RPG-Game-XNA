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
    public class CombatMenuScreen : GameStateScreen
    {
        private string[] Texts;
        private int selected;
        private Rectangle FirstRect, SecondRect, SecondRectSrc;
        private Vector2 StringPos;
        private int IncrementY;
        private Character Attacker;
        private List<Character> Enemies;
        private List<Character> Group;

        public CombatMenuScreen(Character Attacker, List<Character> Enemies, List<Character> Group)
            : base()
        {
            this.Attacker = Attacker;
            this.Enemies = Enemies;
            this.Group = Group;

            selected = 0;
            Texts = new string[3];
            Texts[0] = "Attack";
            Texts[1] = "Skill";
            Texts[2] = "Item";
            int posX = 200;
            int posY = 200;
            int w = 150;
            int h = 190;
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
            for (int i = 0; i < Texts.Length; i++)
            {
                if(selected == i)
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Texts[i], StringPos, Color.DarkRed);
                else
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Texts[i], StringPos, Color.LightGray);
                StringPos.Y += IncrementY;
            }
            StringPos.Y -= Texts.Length * IncrementY;
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Texts.Length)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Texts.Length - 1;
            }

            if (input.IsMenuSelect())
            {
                switch (selected)
                {
                    case 0: // Attack
                        GameStateManager.Instance.AddScreen(new CombatVictimSelectScreen(Attacker, Enemies, Group, "Attack"), true, false);
                        break;
                    case 1: // Skill
                        if (Attacker.Skills.Count > 0)
                        {
                            GameStateManager.Instance.AddScreen(new CombatSkillSelectScreen(Attacker, Enemies, Group), true, false);
                        }
                        break;
                    case 2: // Item
                        GameStateManager.Instance.AddScreen(new CombatConsumableSelectScreen(Attacker, Group), true, false);
                        break;
                }
            }

            if (input.IsBack())
            {
                this.Attacker.ATB -= 50;
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
