using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;
using RPG_Game_XNA.TileEngine;
using RPGData;
namespace RPG_Game_XNA.Menu
{
    public class MenuEntryTestTileEngine : IMenuEntry
    {
        public string GetText() { return "Test TileEngine"; }
        public void Select()
        {
            Map map = Globals.Instance.Content.Load<Map>("Map001");
            GameStateManager.Instance.AddScreen(new TileEngineScreen(map), true, false);
        }
    }
}
