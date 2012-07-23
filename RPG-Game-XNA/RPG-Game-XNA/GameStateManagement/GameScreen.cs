using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameStateManagement
{
    public abstract class GameScreen
    {
        public bool Active;

        public GameScreen()
        {
            Active = false;
        }

        /// <summary>
        /// returns true to prevent bottom screens from updating
        /// </summary>
        public bool Update(GameTime time)
        {
            return false;
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
