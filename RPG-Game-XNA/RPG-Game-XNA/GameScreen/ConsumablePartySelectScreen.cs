﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RPGData;

namespace RPG_Game_XNA.GameScreen
{
    public class ConsumablePartySelectScreen : GameStateScreen
    {
        protected List<Character> Entries;
        protected Texture2D Background;
        private int selected;
        private Consumable Consumable;

        public ConsumablePartySelectScreen(Consumable Consumable)
        {
            this.Consumable = Consumable;
            selected = 0;
            Entries = Session.currentSession.Party;
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Point Size = new Point(500, 175);
            int StartPosY = (int)(Globals.Instance.ScreenHeightHalf - ((float)Entries.Count / 2.0f) * Size.Y);
            Point Position = new Point((int)Globals.Instance.ScreenWidthHalf - (Size.X / 2), StartPosY);
            Globals.Instance.SpriteBatch.Begin();
            for (int i = 0; i < Entries.Count; i++)
            {
                if (i == selected)
                    DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Wheat, Entries[i]);
                else
                    DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Gray, Entries[i]);

                Position.Y += Size.Y;
            }
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Entries.Count)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Entries.Count - 1;
            }

            if (input.IsMenuSelect())
            {
                if (Session.currentSession.Inventory.IsUseable(Consumable, Entries[selected]))
                {
                    ScriptEngine.ScriptEngine.Instance.SetVar("PartySelect", selected);
                    ScriptEngine.ScriptEngine.Instance.Execute(Consumable.Action);
                    Session.currentSession.Inventory.RemoveItem(Consumable, 1);
                    GameStateManager.Instance.RemoveScreen(this);
                }
            }
            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
