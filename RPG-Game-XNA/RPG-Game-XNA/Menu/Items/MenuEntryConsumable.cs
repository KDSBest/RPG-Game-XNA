using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;
using RPG_Game_XNA.TileEngine;
using RPGData;
namespace RPG_Game_XNA.Menu.Items
{
    public class MenuEntryConsumable : MenuEntryItem
    {
        public MenuEntryConsumable(Consumable Item, int Count)
            : base((Item)Item, Count)
        {
        }

        public override void Select()
        {
            ScriptEngine.ScriptEngine.Instance.SetVar("Consumable", Item.Name);
            GameStateManager.Instance.AddScreen(new ConsumablePartySelectScreen(((Consumable)Item)), true, false);
        }
    }
}
