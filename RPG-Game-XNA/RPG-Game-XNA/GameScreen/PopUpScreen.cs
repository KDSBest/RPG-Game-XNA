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
        private Vector2 StringPos;

        public PopUpScreen(string[] Texts)
            : base()
        {
            this.Texts = Texts;
            currentText = 0;
            StringPos = new Vector2(15, 15);
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Globals.Instance.SpriteBatch.Begin();
            DrawHelper.Instance.DrawWindow(new Point(0, 0), new Point((int) Globals.Instance.ScreenWidth, 200), Color.DarkOrange);
            DrawHelper.Instance.DrawText(Texts[currentText], StringPos, Color.LightGray, true, Color.Black);
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
