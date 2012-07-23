using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_Game_XNA.GameScreen
{
    public class PopUpScreen : GameStateScreen
    {
        private string[] Texts;
        private int currentText;
        private Rectangle FirstRect, SecondRect, SecondRectSrc;
        private Vector2 StringPos;

        public PopUpScreen(string[] Texts)
            : base()
        {
            this.Texts = Texts;
            currentText = 0;
            FirstRect = new Rectangle(10, 10, (int)Globals.Instance.ScreenWidth - 20, 200);
            SecondRect = new Rectangle(12, 12, (int)Globals.Instance.ScreenWidth - 24, 196);
            SecondRectSrc = new Rectangle(700, 800, 300, 200);
            StringPos = new Vector2(15, 15);
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Globals.Instance.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, FirstRect, Color.White);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.Gardient, SecondRect, SecondRectSrc, Color.Blue);
            Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, Texts[currentText], StringPos, Color.LightGray);
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuSelect())
                currentText++;
            if (currentText >= Texts.Length)
                GameStateManager.Instance.RemoveScreen(this);
            return true;
        }
    }
}
