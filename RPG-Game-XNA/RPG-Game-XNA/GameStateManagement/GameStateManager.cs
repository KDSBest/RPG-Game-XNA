using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.GameStateManagement
{
    public class GameStateManager
    {
        private List<GameStateScreen> Screens;
        private List<GameStateScreen> UpdateScreens;
        private InputState _input;
        public bool Exit;

        private GameStateManager()
        {
            Screens = new List<GameStateScreen>();
            UpdateScreens = new List<GameStateScreen>();
            _input = new InputState();
            Exit = false;
        }

        public void Update(GameTime time)
        {
            _input.Update();

            UpdateScreens.Clear();

            foreach (GameStateScreen screen in Screens)
                UpdateScreens.Add(screen);
            
            bool inputSended = false;
            for (int i = UpdateScreens.Count - 1; i >= 0; i--)
            {
                if (UpdateScreens[i].Active)
                {
                    if (!inputSended && UpdateScreens[i].HandleInputs(_input))
                        inputSended = true;
                    if (UpdateScreens[i].Update(time))
                        return;
                }
            }
        }

        public void Draw(GameTime time)
        {
            foreach (GameStateScreen screen in Screens)
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
                foreach (GameStateScreen screen in Screens)
                    screen.Active = false;
            gameScreen.Active = ActivateScreen;
            Screens.Add(gameScreen);
        }

        public void ActivateScreen(GameStateScreen gameScreen, bool DeactivateOthers)
        {
            if (DeactivateOthers)
            {
                foreach (GameStateScreen screen in Screens)
                {
                    if (screen != gameScreen)
                        screen.Active = false;
                    else
                        screen.Active = true;
                }
            }
            else
            {
                foreach (GameStateScreen screen in Screens)
                {
                    if (screen == gameScreen)
                        screen.Active = true;
                }
            }
        }

        public void RemoveScreen(GameStateScreen gameScreen)
        {
            Screens.Remove(gameScreen);
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
