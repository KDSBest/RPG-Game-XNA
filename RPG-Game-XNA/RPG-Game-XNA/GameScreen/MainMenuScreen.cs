using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Menu;
using RPG_Game_XNA.AbstractGameScreen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPG_Game_XNA.GameScreen
{
    public class MainMenuScreen : MenuScreen
    {
        private Texture2D tex;
        private Texture2D[] entryTex;

        public MainMenuScreen()
            : base()
        {
            Entries.Add(new MenuEntryNewGame());
            Entries.Add(new MenuEntryLoadGame());
            Entries.Add(new MenuEntryQuit());
            tex = Globals.Instance.Content.Load<Texture2D>("Main Menu");
            entryTex = new Texture2D[3];
            entryTex[0] = Globals.Instance.Content.Load<Texture2D>("NewGame");
            entryTex[1] = Globals.Instance.Content.Load<Texture2D>("Load");
            entryTex[2] = Globals.Instance.Content.Load<Texture2D>("Quit");
        }

        public override void Draw(GameTime time)
        {
            Vector2 size = Globals.Instance.SpriteFont.MeasureString("A");
            float MenuIncrementY = size.Y + Space + 10;
            float MenuSize = MenuIncrementY * Entries.Count;
            Vector2 drawPosition = Vector2.Zero;

            Globals.Instance.SpriteBatch.Begin();
            Globals.Instance.SpriteBatch.Draw(tex, Globals.Instance.FullScreenRectangle, Color.LightGray);
//            Globals.Instance.SpriteBatch.Draw(loadGameTex, Globals.Instance.FullScreenRectangle, Color.LightGray);
//            Globals.Instance.SpriteBatch.Draw(quitTex, Globals.Instance.FullScreenRectangle, Color.LightGray);

            string text = "";
            Vector2 textSize;
            drawPosition.Y = Globals.Instance.ScreenHeightHalf - MenuSize / 2 + 25;
            drawPosition.X = Globals.Instance.ScreenWidthHalf + 130;
            for (int i = 0; i < Entries.Count; i++)
            {
                text = Entries[i].GetText();
                textSize = Globals.Instance.SpriteFont.MeasureString(text);
                if (i == selected)
                {
                    Globals.Instance.SpriteBatch.Draw(entryTex[i], drawPosition - new Vector2(35, 3), null, Color.White, 0, Vector2.Zero, 0.05f, SpriteEffects.None, 0);
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                }
                else
                {
                    Globals.Instance.SpriteBatch.Draw(entryTex[i], drawPosition - new Vector2(35, 3), null, Color.DarkGray, 0, Vector2.Zero, 0.05f, SpriteEffects.None, 0);
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.DarkGray);
                }
                drawPosition.Y += MenuIncrementY;
            }

            Globals.Instance.SpriteBatch.End();
        }
    }
}
