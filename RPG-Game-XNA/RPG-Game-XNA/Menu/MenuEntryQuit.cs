using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;

namespace RPG_Game_XNA.Menu
{
    public class MenuEntryQuit : IMenuEntry
    {
        public string GetText() { return "Quit"; }
        public void Select() 
        {
            GameStateManager.Instance.Exit = true;
        }
    }
}
