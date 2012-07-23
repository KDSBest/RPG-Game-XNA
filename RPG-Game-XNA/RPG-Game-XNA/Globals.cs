using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPG_Game_XNA
{
    public class Globals
    {
        public ContentManager Content;
        public GraphicsDevice Graphics;
        public SpriteBatch SpriteBatch;
        public SpriteFont SpriteFont;
        public float ScreenHeight;
        public float ScreenWidth;
        public float ScreenHeightHalf;
        public float ScreenWidthHalf;
        public Texture2D PixelWhite;

        public Globals()
        {
        }

        public void Initialize(ContentManager Content, GraphicsDevice Graphics)
        {
            this.Content = Content;
            this.Graphics = Graphics;
            this.SpriteBatch = new SpriteBatch(Graphics);
            this.SpriteFont = Content.Load<SpriteFont>("DefaultFont");
            
            ScreenHeight = Graphics.Viewport.Height;
            ScreenWidth = Graphics.Viewport.Width;
            ScreenHeightHalf = ScreenHeight / 2;
            ScreenWidthHalf = ScreenWidth / 2;

            PixelWhite = new Texture2D(Graphics, 1, 1);
            Color[] data = { Color.White };
            PixelWhite.SetData<Color>(data);
        }

        #region Singleton pattern
        private static Globals _instance;
        public static Globals Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Globals();
                return _instance;
            }
        }
        #endregion
    }
}
