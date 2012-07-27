﻿using System;
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
    public class MenuEntryNewGame : IMenuEntry
    {
        public string GetText() { return "New Game"; }
        public void Select()
        {
            Session.currentSession = new Session();
            GameStateManager.Instance.AddScreen(new TileEngineScreen(), true, false);
        }
    }
}
