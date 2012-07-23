#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
#endregion

namespace RPG_Game_XNA.GameStateManagement
{
    public class InputState
    {
        private KeyboardState CurrentKeyboardStates;
        private KeyboardState LastKeyboardStates;

        public InputState()
        {
        }

        public void Update()
        {
            LastKeyboardStates = CurrentKeyboardStates;
            CurrentKeyboardStates = Keyboard.GetState();
        }

        public bool IsNewKeyPress(Keys key)
        {
            return (CurrentKeyboardStates.IsKeyDown(key) &&
                    LastKeyboardStates.IsKeyUp(key));
        }

        public bool IsMenuSelect()
        {
            return IsNewKeyPress(Keys.Space) || IsNewKeyPress(Keys.Enter);
        }

        public bool IsMenuUp()
        {
            return IsNewKeyPress(Keys.Up);
        }

        public bool IsMenuDown()
        {
            return IsNewKeyPress(Keys.Down);
        }

        public bool IsNext()
        {
            return IsNewKeyPress(Keys.Right);
        }

        public bool IsPrevious()
        {
            return IsNewKeyPress(Keys.Left);
        }

        public bool IsPauseGame()
        {
            return IsNewKeyPress(Keys.Escape);
        }
    }
}
