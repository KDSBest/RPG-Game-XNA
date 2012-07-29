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
    public class CombatConsumableSelectScreen : GameStateScreen
    {
        private int selected;
        private Rectangle FirstRect, SecondRect, SecondRectSrc;
        private Vector2 StringPos;
        private int IncrementY;
        private Character Attacker;
        private List<Character> Group;
        private List<Consumable> Items;
        private List<int> ItemCounts;

        public CombatConsumableSelectScreen(Character Attacker, List<Character> Group)
            : base()
        {
            this.Attacker = Attacker;
            this.Group = Group;

            Dictionary<Consumable, int> Items = Session.currentSession.Inventory.GetConsumables();
            this.Items = new List<Consumable>();
            this.ItemCounts = new List<int>();
            foreach (Consumable Item in Items.Keys)
            {
                this.Items.Add(Item);
                ItemCounts.Add(Items[Item]);
            }
            selected = 0;

            int posX = 300;
            int posY = 100;
            int w = 300;
            int h = 400;
            FirstRect = new Rectangle(posX, posY, w, h);
            SecondRect = new Rectangle(posX + 2, posY + 2, w - 4, h - 4);
            SecondRectSrc = new Rectangle(700, 800, 300, 200);
            StringPos = new Vector2(posX + 15, posY + 15);
            IncrementY = (int) Globals.Instance.SpriteFont.MeasureString("A").Y + 10;
        }

        public override bool Update(GameTime time)
        {
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);
            Globals.Instance.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, FirstRect, Color.White);
            Globals.Instance.SpriteBatch.Draw(Globals.Instance.Gardient, SecondRect, SecondRectSrc, Color.Blue);
            float savedY = StringPos.Y;
            for (int i = 0; i < Items.Count; i++ )
            {
                if (selected == i)
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, ItemCounts[i] + " " + Items[i].Name, StringPos, Color.DarkRed);
                else
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, ItemCounts[i] + " " + Items[i].Name, StringPos, Color.LightGray);
                StringPos.Y += IncrementY;
            }
            StringPos.Y = savedY;
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            if (input.IsMenuDown())
            {
                selected++;
                if (selected >= Items.Count)
                    selected = 0;
            }
            if (input.IsMenuUp())
            {
                selected--;
                if (selected < 0)
                    selected = Items.Count - 1;
            }

            if (input.IsMenuSelect())
            {
                GameStateManager.Instance.AddScreen(new CombatConsumablePartySelectScreen(Attacker, Group, Items[selected]), true, false);
            }

            if (input.IsBack())
            {
                GameStateManager.Instance.RemoveScreen(this);
            }
            return true;
        }
    }
}
