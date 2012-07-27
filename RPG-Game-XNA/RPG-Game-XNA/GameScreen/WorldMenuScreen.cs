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
        }

        public override bool HandleInputs(InputState input)
        {
            base.HandleInputs(input);
            if (input.IsBack())
                GameStateManager.Instance.RemoveScreen(this);
            return true;
        }
    
    }
}
