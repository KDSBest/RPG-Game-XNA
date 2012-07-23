using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameStateManagement
{
    public class GameScreenManager
    {
        private List<GameScreen> _screens;
        private InputState _input;

        private GameScreenManager()
        {
            _screens = new List<GameScreen>();
            _input = new InputState();
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
            foreach (GameScreen screen in _screens)
            {
                if (screen.Active)
                {
                    screen.Draw(time);
                }
            }
        }

        public void AddScreen(GameScreen gameScreen, bool ActivateScreen, bool DeactivateOthers)
        {
            if (DeactivateOthers)
                foreach (GameScreen screen in _screens)
                    screen.Active = false;
            gameScreen.Active = ActivateScreen;
            _screens.Add(gameScreen);
        }

        public void ActivateScreen(GameScreen gameScreen, bool DeactivateOthers)
        {
            if (DeactivateOthers)
            {
                foreach (GameScreen screen in _screens)
                {
                    if (screen != gameScreen)
                        screen.Active = false;
                    else
                        screen.Active = true;
                }
            }
            else
            {
                foreach (GameScreen screen in _screens)
                {
                    if (screen == gameScreen)
                        screen.Active = true;
                }
            }
        }

        #region Singleton pattern
        private GameScreenManager _instance;
        public GameScreenManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameScreenManager();
                return _instance;
            }
        }
        #endregion
    }
}
