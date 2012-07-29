using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGData;

namespace RPG_Game_XNA.GameScreen
{
    public class CombatRewardScreen : GameStateScreen
    {
        private Dictionary<string, int> Items;
        private int CombatExp;
        public CombatRewardScreen(int CombatExp, List<ItemDrop> Items)
            : base()
        {
            this.CombatExp = CombatExp;
            this.Items = new Dictionary<string,int>();
            foreach (ItemDrop id in Items)
            {
                int Random = Globals.Instance.Random.Next(100);
                if (Random < id.Chance)
                {
                    Session.currentSession.Inventory.AddItem(id.ItemName, 1);
                    if (this.Items.ContainsKey(id.ItemName))
                        this.Items[id.ItemName]++;
                    else
                        this.Items.Add(id.ItemName, 1);
                }
            }
            foreach (Character c in Session.currentSession.Party)
                c.Experience += CombatExp;
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            string text = "EXP: " + CombatExp + "\n";
            foreach (string ItemName in Items.Keys)
            {
                text += Items[ItemName] + " " + ItemName + "\n";
            }

            Globals.Instance.SpriteBatch.Begin();
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, Globals.Instance.FullScreenRectangle, Color.Black);
            Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, Vector2.One, Color.LightGray);
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuSelect() || input.IsBack())
                GameStateManager.Instance.BackToTileScreen();
            return true;
        }
    }
}
