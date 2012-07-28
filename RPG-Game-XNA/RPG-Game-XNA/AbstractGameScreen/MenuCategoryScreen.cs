using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPG_Game_XNA.Interfaces;
using RPG_Game_XNA.GameStateManagement;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA.AbstractGameScreen
{
    public abstract class MenuCategoryScreen : GameStateScreen
    {
        private Dictionary<string, List<IMenuEntry>> Menu;
        protected Texture2D Background;
        private int selected;
        private int selectedCategory;
        public float Space;
        private float Pulsate;
        private Vector2 ScalePulsate;
        private List<string> Keys;

        public MenuCategoryScreen()
        {
            selected = 0;
            selectedCategory = 0;
            Space = 10.0f;
            Pulsate = 0;
            ScalePulsate = Vector2.One;
            Menu = new Dictionary<string, List<IMenuEntry>>();
            Keys = new List<string>();
        }

        public void AddCategory(string Category)
        {
            if (!Menu.ContainsKey(Category))
            {
                Menu.Add(Category, new List<IMenuEntry>());
                if(!Keys.Contains(Category))
                    Keys.Add(Category);
            }
        }

        public void Clear()
        {
            Menu.Clear();
            foreach (string Category in Keys)
                AddCategory(Category);
        }

        public void AddEntry(string Category, IMenuEntry Entry)
        {
            if (!Menu.ContainsKey(Category))
            {
                AddCategory(Category);
            }
            Menu[Category].Add(Entry);
        }

        public override bool Update(GameTime time)
        {
            Pulsate = (float)Math.Sin(time.TotalGameTime.TotalMilliseconds / 100);
            ScalePulsate += new Vector2(Pulsate / 20, Pulsate / 20);
            return true;
        }

        public override void Draw(GameTime time)
        {
            base.Draw(time);

            List<IMenuEntry> Entries = Menu[Keys[selectedCategory]];
            Vector2 size = Globals.Instance.SpriteFont.MeasureString("A");
            float MenuIncrementY = size.Y + Space;
            float MenuSize = MenuIncrementY * Entries.Count;
            Vector2 drawPosition = Vector2.Zero;
            
            Globals.Instance.SpriteBatch.Begin();

            Globals.Instance.SpriteBatch.Draw(Globals.Instance.PixelWhite, Globals.Instance.FullScreenRectangle, Color.Black);
            string text = "";
            Vector2 textSize;
            drawPosition.Y = Globals.Instance.ScreenHeightHalf - MenuSize / 2;
            if (selected >= Entries.Count)
                selected = Entries.Count - 1;
            if (selected < 0)
                selected = 0;
            for (int i = 0; i < Entries.Count; i++)
            {
                text = Entries[i].GetText();
                textSize = Globals.Instance.SpriteFont.MeasureString(text);
                drawPosition.X = Globals.Instance.ScreenWidthHalf - textSize.X / 2;
                if (i == selected)
                {
                    Vector2 Center = new Vector2(textSize.X / 2, size.Y / 2);
                    drawPosition += Center;
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.Blue, 0, Center, ScalePulsate, SpriteEffects.None, 0);
                    drawPosition -= Center;
                }
                else
                {
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.White);
                }
                drawPosition.Y += MenuIncrementY;
            }

            drawPosition.Y = 0;
            drawPosition.X = 0;
            for (int i = 0; i < Keys.Count; i++)
            {
                text = Keys[i];
                textSize = Globals.Instance.SpriteFont.MeasureString(text);
                if (i == selectedCategory)
                {
                    Vector2 Center = new Vector2(textSize.X / 2, size.Y / 2);
                    drawPosition += Center;
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.Blue, 0, Center, ScalePulsate, SpriteEffects.None, 0);
                    drawPosition -= Center;
                }
                else
                {
                    Globals.Instance.SpriteBatch.DrawString(Globals.Instance.SpriteFont, text, drawPosition, Color.White);
                }
                drawPosition.X += textSize.X + Space;
            }
            Globals.Instance.SpriteBatch.End();
        }

        public override bool HandleInputs(InputState input)
        {
            List<IMenuEntry> Entries = Menu[Keys[selectedCategory]];
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
                Entries[selected].Select();
            if (input.IsNext())
            {
                selected = 0;
                selectedCategory++;
                if (selectedCategory >= Keys.Count)
                    selectedCategory = 0;
            }
            if (input.IsPrevious())
            {
                selected = 0;
                selectedCategory--;
                if (selectedCategory < 0)
                    selectedCategory = Keys.Count - 1;
            }
            return true;
        }
    }
}
