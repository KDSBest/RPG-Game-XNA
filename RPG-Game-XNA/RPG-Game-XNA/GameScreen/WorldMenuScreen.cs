using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Menu;
using RPG_Game_XNA.AbstractGameScreen;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameScreen
{
    public class WorldMenuScreen : MenuScreen
    {
        public WorldMenuScreen()
            : base()
        {
            Entries.Add(new MenuEntryInventory());
            Entries.Add(new MenuEntrySaveGame());
            Entries.Add(new MenuEntryQuit());
        }

        public override bool HandleInputs(InputState input)
        {
            base.HandleInputs(input);
            if (input.IsBack())
                GameStateManager.Instance.RemoveScreen(this);
            return true;
        }

        public override void Draw(GameTime time)
        {
            Globals.Instance.SpriteBatch.Begin();
            DrawHelper.Instance.DrawFullScreenColor(Color.Black * 0.9f);
            Point Position = new Point(0, 50);
            Point Size = new Point((int)Globals.Instance.ScreenWidth / 3, 550);
            DrawHelper.Instance.DrawWindow(new Point(0, 0), new Point(300, 50), Color.DarkGray);
            DrawHelper.Instance.DrawText(Entries[selected].GetText(), new Vector2(25, 12), Color.White, true, Color.Black);
            for (int i = 0; i < 3; i++)
            {
                if (i < Session.currentSession.Party.Count)
                    DrawHelper.Instance.DrawCharacterInfo(Position, Size, Color.Wheat, Session.currentSession.Party[i]);
                else
                    DrawHelper.Instance.DrawWindow(Position, Size, Color.DarkGray);
                Position.X += Size.X;
            }
            Globals.Instance.SpriteBatch.End();
        }
    }
}
