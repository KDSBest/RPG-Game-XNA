using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Menu;
using RPG_Game_XNA.AbstractGameScreen;
using RPG_Game_XNA.GameStateManagement;

namespace RPG_Game_XNA.GameScreen
{
    public class SaveGameScreen : MenuScreen
    {
        public SaveGameScreen()
            : base()
        {
            for(int i = 0; i < 10; i++)
                Entries.Add(new MenuEntrySaveGameEntry(i));
        }

        public override bool HandleInputs(InputState input)
        {
            base.HandleInputs(input);

            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
