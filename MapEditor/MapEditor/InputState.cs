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
        public KeyboardState CurrentKeyboardStates;
        public KeyboardState LastKeyboardStates;
        public MouseState CurrentMouseStates;
        public MouseState LastMouseState;

        public InputState()
        {
        }

        public void Update()
        {
            LastKeyboardStates = CurrentKeyboardStates;
            CurrentKeyboardStates = Keyboard.GetState();
            LastMouseState = CurrentMouseStates;
            CurrentMouseStates = Mouse.GetState();
        }

        public bool IsNewKeyPress(Keys key)
        {
            return (CurrentKeyboardStates.IsKeyDown(key) &&
                    LastKeyboardStates.IsKeyUp(key));
        }

        public bool IsKeyDown(Keys key)
        {
            return (CurrentKeyboardStates.IsKeyDown(key));
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

        public bool IsBack()
        {
            return IsNewKeyPress(Keys.Escape);
        }

        public bool IsMenu()
        {
            return IsNewKeyPress(Keys.Escape);
        }

        public bool IsUp()
        {
            return IsKeyDown(Keys.Up);
        }
        public bool IsDown()
        {
            return IsKeyDown(Keys.Down);
        }
        public bool IsLeft()
        {
            return IsKeyDown(Keys.Left);
        }
        public bool IsRight()
        {
            return IsKeyDown(Keys.Right);
        }
        
    }
}
