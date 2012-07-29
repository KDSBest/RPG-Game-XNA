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
    public class MenuEntrySaveGameEntry : IMenuEntry
    {
        private int Nr;
        public bool HasData;
        public string Name;

        public MenuEntrySaveGameEntry(int Nr)
        {
            this.Nr = Nr;
            Name = "SaveGame " + Nr;
            if (Globals.Instance.StorageContainer.FileExists(Name))
            {
                HasData = true;
            }
        }

        public string GetText() 
        {
            if (HasData)
                return Name;
            else
                return "No Savegame";
        }
        
        public void Select()
        {
            if(Session.currentSession.SaveGame(Name))
                GameStateManager.Instance.BackToTileScreen();
        }
    }
}
