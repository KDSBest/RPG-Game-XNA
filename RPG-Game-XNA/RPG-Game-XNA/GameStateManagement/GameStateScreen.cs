using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameStateManagement
{
    public abstract class GameStateScreen
    {
        public bool Active;

        public GameStateScreen()
        {
            Active = false;
        }


        /// returns true to prevent bottom screens from updating
        
        public virtual bool Update(GameTime time)
        {
            return false;
        }

        public virtual void Draw(GameTime time)
        {
        }

        public virtual bool HandleInputs(InputState input)
        {
            // Give away Key inputs
            return false;
        }
    }
}
