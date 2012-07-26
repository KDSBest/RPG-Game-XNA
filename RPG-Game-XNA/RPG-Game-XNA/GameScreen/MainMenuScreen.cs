using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Menu;
using RPG_Game_XNA.AbstractGameScreen;

namespace RPG_Game_XNA.GameScreen
{
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen() : base()
        {
            Entries.Add(new MenuEntryNewGame());
            Entries.Add(new MenuEntryTestPopUp());
            Entries.Add(new MenuEntryQuit());
        }
    }
}
