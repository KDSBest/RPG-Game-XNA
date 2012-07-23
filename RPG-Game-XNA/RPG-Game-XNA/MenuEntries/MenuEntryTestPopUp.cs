using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;

namespace RPG_Game_XNA.Menu
{
    public class MenuEntryTestPopUp : IMenuEntry
    {
        public string GetText() { return "Test PopUp"; }
        public void Select() 
        {
            string[] Texts = { "Test", "Test2\n2.Line" };
            GameStateManager.Instance.AddScreen(new PopUpScreen(Texts), true, false);
        }
    }
}
