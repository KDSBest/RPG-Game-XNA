using System;
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
    public class ArmourPartySelectScreen : GameStateScreen
    {
        protected List<Character> Entries;
        protected Texture2D Background;
        private int selected;
        private Armour Armour;

        public ArmourPartySelectScreen(Armour Armour)
        {
            this.Armour = Armour;
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
            Point Size = new Point(630, 175);
            int StartPosY = (int)(Globals.Instance.ScreenHeightHalf - ((float)Entries.Count / 2.0f) * Size.Y);
            Point Position = new Point((int)Globals.Instance.ScreenWidthHalf - (Size.X / 2), StartPosY);
            Globals.Instance.SpriteBatch.Begin();
            for (int i = 0; i < Entries.Count; i++)
            {
                if (i == selected)
                    DrawHelper.Instance.DrawSelectArmourCharacterInfo(Position, Size, Color.Wheat, Entries[i], Armour);
                else
                    DrawHelper.Instance.DrawSelectArmourCharacterInfo(Position, Size, Color.Gray, Entries[i], Armour);

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
                Session.currentSession.Inventory.AddItem(Entries[selected].Armour, 1);
                Session.currentSession.Inventory.RemoveItem(Armour, 1);
                Entries[selected].Armour = Armour;
                GameStateManager.Instance.RemoveScreen(this);
            }
            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
