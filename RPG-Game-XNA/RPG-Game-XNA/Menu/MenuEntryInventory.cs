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
    public class MenuEntryInventory : IMenuEntry
    {
        public string GetText() { return "Inventory"; }
        public void Select()
        {
            GameStateManager.Instance.AddScreen(new InventoryScreen(), true, false);
        }
    }
}
