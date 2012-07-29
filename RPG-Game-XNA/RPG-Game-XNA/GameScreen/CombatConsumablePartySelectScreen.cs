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
    public class CombatConsumablePartySelectScreen : GameStateScreen
    {
        private int selected;
        private Rectangle FirstRect, SecondRect, SecondRectSrc;
        private Vector2 StringPos;
        private int IncrementY;
        private Character Attacker;
        private List<Character> Group;
        private Consumable Consumable;

        public CombatConsumablePartySelectScreen(Character Attacker, List<Character> Group, Consumable Consumable)
            : base()
        {
            this.Attacker = Attacker;
            this.Group = Group;
            this.Consumable = Consumable;

            selected = 0;

            int posX = 400;
            int posY = 100;
            int w = 600;
            int h = 600;
            FirstRect = new Rectangle(posX, posY, w, h);
            SecondRect = new Rectangle(posX + 2, posY + 2, w - 4, h - 4);
            SecondRectSrc = new Rectangle(700, 800, 300, 200);
            StringPos = new Vector2(posX + 15, posY + 15);
            IncrementY = (int) Globals.Instance.SpriteFont.MeasureString("A\nHP\nMP").Y + 10;
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
            float savedY = StringPos.Y;
            for (int i = 0; i < Group.Count; i++)
            {
                string text = Group[i].DisplayName + "\nHP: " + Group[i].HP + "/" + Group[i].MaxHP + "\nMP: " + Group[i].MP + "/" + Group[i].MaxMP;
                if (selected == i)
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, StringPos, Color.DarkRed);
                else
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, StringPos, Color.LightGray);
                StringPos.Y += IncrementY;
            }
            StringPos.Y = savedY;;
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
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
                if (Session.currentSession.Inventory.IsUseable(Consumable, Group[selected]))
                {
                    Attacker.ATB = 0;
                    ScriptEngine.ScriptEngine.Instance.SetVar("PartySelect", selected);
                    ScriptEngine.ScriptEngine.Instance.Execute(Consumable.Action);
                    Session.currentSession.Inventory.RemoveItem(Consumable, 1);
                    GameStateManager.Instance.BackToCombatScreen();
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
