using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGData;
using RPG_Game_XNA.Combat;

namespace RPG_Game_XNA.GameScreen
{
    public class CombatConsumablePartySelectScreen : GameStateScreen
    {
        private int selected;
        private Character Attacker;
        private List<Character> Group;
        private Consumable Consumable;

        public CombatConsumablePartySelectScreen(Character Attacker, List<Character> Group, Consumable Consumable)
            : base()
        {
            this.Attacker = Attacker;
            this.Group = Group;
            this.Consumable = Consumable;

            selected = 0;
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Globals.Instance.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Point Size = new Point(500, 175);
            int StartPosY = (int)(Globals.Instance.ScreenHeightHalf - ((float)Group.Count / 2.0f) * Size.Y);
            Point Position = new Point((int)Globals.Instance.ScreenWidthHalf - (Size.X / 2), StartPosY);
            Globals.Instance.SpriteBatch.Begin();
            for (int i = 0; i < Group.Count; i++)
            {
                if (i == selected)
                    DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Wheat, Group[i]);
                else
                    DrawHelper.Instance.DrawSelectCharacterInfo(Position, Size, Color.Gray, Group[i]);

                Position.Y += Size.Y;
            }
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Group.Count)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Group.Count - 1;
            }
            if (input.IsMenuSelect())
            {
                if (Session.currentSession.Inventory.IsUseable(Consumable, Group[selected]))
                {
                    Attacker.ATB = 0;
                    ScriptEngine.ScriptEngine.Instance.SetVar("PartySelect", selected);
                    ScriptEngine.ScriptEngine.Instance.Execute(Consumable.Action);
                    Session.currentSession.Inventory.RemoveItem(Consumable, 1);
                    GameStateManager.Instance.BackToCombatScreen();
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
