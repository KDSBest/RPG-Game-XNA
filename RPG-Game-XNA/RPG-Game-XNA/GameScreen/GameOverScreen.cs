using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_Game_XNA.GameScreen
{
    public class GameOverScreen : GameStateScreen
    {
        public GameOverScreen()
            : base()
        {
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Globals.Instance.SpriteBatch.Begin();
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, Globals.Instance.FullScreenRectangle, Color.Black);
            Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, "GAME OVER!", Vector2.One, Color.LightGray);
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuSelect() || input.IsBack())
                GameStateManager.Instance.RemoveScreen(this);
            return true;
        }
    }
}
