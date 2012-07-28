using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RPGData;

namespace RPG_Game_XNA.GameScreen
{
    public class PartySelectScreen : GameStateScreen
    {
        protected List<Character> Entries;
        protected Texture2D Background;
        private int selected;
        public float Space;
        private float Pulsate;
        private Vector2 ScalePulsate;
        private List<ScriptEngineCommand> Action;

        public PartySelectScreen(List<ScriptEngineCommand> Action)
        {
            this.Action = Action;
            selected = 0;
            Space = 10.0f;
            Pulsate = 0;
            ScalePulsate = Vector2.One;
            Entries = Session.currentSession.Party;
        }

        public override bool Update(GameTime time)
        {
            Pulsate = (float)Math.Sin(time.TotalGameTime.TotalMilliseconds / 100);
            ScalePulsate += new Vector2(Pulsate / 20, Pulsate / 20);
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Vector2 size = Globals.Instance.SpriteFont.MeasureString("Name\nHP\nMP\nEXP");
            float MenuIncrementY = size.Y + Space;
            float MenuSize = MenuIncrementY * Entries.Count;
            Vector2 drawPosition = Vector2.Zero;

            Globals.Instance.SpriteBatch.Begin();
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, Globals.Instance.FullScreenRectangle, Color.Black);

            string text = "";
            Vector2 textSize;
            drawPosition.Y = Globals.Instance.ScreenHeightHalf - MenuSize / 2;
            for (int i = 0; i < Entries.Count; i++)
            {
                text = "Name: " + Entries[i].Name + " (" + Entries[i].Level + ")\n";
                text += "HP: " + Entries[i].HP + "/" + Entries[i].MaxHP + "\n";
                text += "MP: " + Entries[i].MP + "/" + Entries[i].MaxMP + "\n";
                text += "EXP: " + Entries[i].Experience + "/" + Entries[i].ExperienceForNextLevel();
                textSize = Globals.Instance.SpriteFont.MeasureString(text);
                drawPosition.X = Globals.Instance.ScreenWidthHalf - textSize.X / 2;
                if (i == selected)
                {
                    Vector2 Center = new Vector2(textSize.X / 2, size.Y / 2);
                    drawPosition += Center;
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.Blue, 0, Center, ScalePulsate, SpriteEffects.None, 0);
                    drawPosition -= Center;
                }
                else
                {
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.White);
                }
                drawPosition.Y += MenuIncrementY;
            }

            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Entries.Count)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Entries.Count - 1;
            }

            if (input.IsMenuSelect())
            {
                ScriptEngine.ScriptEngine.Instance.SetVar("PartySelect", selected);
                ScriptEngine.ScriptEngine.Instance.Execute(Action);
                GameStateManager.Instance.RemoveScreen(this);
            }
            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
