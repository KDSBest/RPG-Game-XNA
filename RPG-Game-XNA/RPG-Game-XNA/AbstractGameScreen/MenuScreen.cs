using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.AbstractGameScreen
{
    public abstract class MenuScreen : GameStateScreen
    {
        protected List<IMenuEntry> Entries;
        protected Texture2D Background;
        private int selected;
        public float Space;
        private float Pulsate;
        private Vector2 ScalePulsate;

        public MenuScreen()
        {
            selected = 0;
            Space = 10.0f;
            Pulsate = 0;
            ScalePulsate = Vector2.One;
            Entries = new List<IMenuEntry>();
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
            Vector2 size = Globals.Instance.SpriteFont.MeasureString("A");
            float MenuIncrementY = size.Y + Space;
            float MenuSize = MenuIncrementY * Entries.Count;
            Vector2 drawPosition = Vector2.Zero;
            
            Globals.Instance.SpriteBatch.Begin();

            string text = "";
            Vector2 textSize;
            drawPosition.Y = Globals.Instance.ScreenHeightHalf - MenuSize / 2;
            for (int i = 0; i < Entries.Count; i++)
            {
                text = Entries[i].GetText();
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
                selected++;
            if (input.IsMenuUp())
                selected--;
            if (selected < 0)
                selected = Entries.Count - 1;
            if (selected >= Entries.Count)
                selected = 0;

            if (input.IsMenuSelect())
                Entries[selected].Select();

            return true;
        }
    }
}
