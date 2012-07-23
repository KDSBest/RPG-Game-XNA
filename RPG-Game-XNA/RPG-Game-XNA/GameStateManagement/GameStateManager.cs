using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameStateManagement
{
    public class GameStateManager
    {
        private List<GameStateScreen> _screens;
        private InputState _input;
        public bool Exit;

        private GameStateManager()
        {
            _screens = new List<GameStateScreen>();
            _input = new InputState();
            Exit = false;
        }

        public void Update(GameTime time)
        {
            _input.Update();
            bool inputSended = false;
            for (int i = _screens.Count - 1; i >= 0; i--)
            {
                if (_screens[i].Active)
                {
                    if (!inputSended && _screens[i].HandleInputs(_input))
                        inputSended = true;
                    if (_screens[i].Update(time))
                        return;
                }
            }
        }

        public void Draw(GameTime time)
        {
            foreach (GameStateScreen screen in _screens)
            {
                if (screen.Active)
                {
                    screen.Draw(time);
                }
            }
        }

        public void AddScreen(GameStateScreen gameScreen, bool ActivateScreen, bool DeactivateOthers)
        {
            if (DeactivateOthers)
                foreach (GameStateScreen screen in _screens)
                    screen.Active = false;
            gameScreen.Active = ActivateScreen;
            _screens.Add(gameScreen);
        }

        public void ActivateScreen(GameStateScreen gameScreen, bool DeactivateOthers)
        {
            if (DeactivateOthers)
            {
                foreach (GameStateScreen screen in _screens)
                {
                    if (screen != gameScreen)
                        screen.Active = false;
                    else
                        screen.Active = true;
                }
            }
            else
            {
                foreach (GameStateScreen screen in _screens)
                {
                    if (screen == gameScreen)
                        screen.Active = true;
                }
            }
        }

        #region Singleton pattern
        private static GameStateManager _instance;
        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameStateManager();
                return _instance;
            }
        }
        #endregion
    }
}
