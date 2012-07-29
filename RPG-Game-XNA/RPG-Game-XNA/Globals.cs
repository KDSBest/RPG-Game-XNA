using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

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
        public Texture2D Gardient;
        public Rectangle FullScreenRectangle;
        public Random Random;

        public StorageDevice StorageDevice;
        private IAsyncResult StorageDeviceResult;
        public StorageContainer StorageContainer;

        public Globals()
        {
        }

        public void OpenContainer()
        {
            StorageDeviceResult = StorageDevice.BeginOpenContainer("RPG_Game_XNA", null, null);
            StorageDeviceResult.AsyncWaitHandle.WaitOne();
            StorageContainer = StorageDevice.EndOpenContainer(StorageDeviceResult);
            StorageDeviceResult.AsyncWaitHandle.Close();
        }

        public void CloseContainer()
        {
            StorageContainer.Dispose();
        }

        public void Initialize(ContentManager Content, GraphicsDevice Graphics)
        {
            StorageDeviceResult = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            StorageDeviceResult.AsyncWaitHandle.WaitOne();
            StorageDevice = StorageDevice.EndShowSelector(StorageDeviceResult);
            StorageDeviceResult.AsyncWaitHandle.Close();
            OpenContainer();
            this.Content = Content;
            this.Graphics = Graphics;
            this.SpriteBatch = new SpriteBatch(Graphics);
            this.SpriteFont = Content.Load<SpriteFont>("DefaultFont");

            ScreenHeight = Graphics.Viewport.Height;
            ScreenWidth = Graphics.Viewport.Width;
            ScreenHeightHalf = ScreenHeight / 2;
            ScreenWidthHalf = ScreenWidth / 2;
            FullScreenRectangle = new Rectangle(0, 0, (int)ScreenWidth, (int)ScreenHeight);

            Random = new Random();
            PixelWhite = new Texture2D(Graphics, 1, 1);
            Color[] data = { Color.White };
            PixelWhite.SetData<Color>(data);
            int GardientSize = 1000;
            Gardient = new Texture2D(Graphics, GardientSize, GardientSize);
            data = new Color[GardientSize * GardientSize];
            for (int x = 0; x < GardientSize; x++)
            {
                for (int y = 0; y < GardientSize; y++)
                {
                    float c = 1.0f - ((float)(x * y)) / (GardientSize * GardientSize);
                    data[x + y * GardientSize] = new Color(c, c, c);
                }
            }
            Gardient.SetData<Color>(data);
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
