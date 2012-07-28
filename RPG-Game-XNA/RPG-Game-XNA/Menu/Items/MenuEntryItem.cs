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
    public abstract class MenuEntryItem : IMenuEntry
    {
        protected Item Item;
        protected int Count;

        public MenuEntryItem(Item Item, int Count)
            : base()
        {
            this.Item = Item;
            this.Count = Count;
        }

        public string GetText() { return Count + " " + Item.Name; }
        public virtual void Select()
        {
        }
    }
}
