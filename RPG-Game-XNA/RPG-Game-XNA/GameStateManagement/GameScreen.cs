using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameStateManagement
{
    public abstract class GameScreen
    {

        public void Update(GameTime time)
        {

        }

        public void Draw(GameTime time)
        {

        }

        public bool HandleInputs(InputState input)
        {
            // Give away Key inputs
            return false;
        }
    }
}
