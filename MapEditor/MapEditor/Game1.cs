using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RPG_Game_XNA;
using RPGData;
using RPG_Game_XNA.GameStateManagement;

namespace MapEditor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        InputState input;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.Instance.Initialize(Content, this.GraphicsDevice);
            Globals.Instance.TileEngine.SetMap(Content.Load<Map>("Maps\\StartLevel"), -1);
            input = new InputState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            input.Update();
            if (input.IsMenuDown())
                Globals.Instance.TileEngine.MapOriginPosition.Y -= Globals.Instance.TileEngine.Map.TileSize.Y;
            if (input.IsMenuUp())
                Globals.Instance.TileEngine.MapOriginPosition.Y += Globals.Instance.TileEngine.Map.TileSize.Y;
            if (input.IsPrevious())
                Globals.Instance.TileEngine.MapOriginPosition.X += Globals.Instance.TileEngine.Map.TileSize.X;
            if (input.IsNext())
                Globals.Instance.TileEngine.MapOriginPosition.X -= Globals.Instance.TileEngine.Map.TileSize.X;
            Globals.Instance.TileEngine.MapOriginPosition.X = MathHelper.Min(Globals.Instance.TileEngine.MapOriginPosition.X, Globals.Instance.TileEngine.Viewport.X);
            Globals.Instance.TileEngine.MapOriginPosition.Y = MathHelper.Min(Globals.Instance.TileEngine.MapOriginPosition.Y, Globals.Instance.TileEngine.Viewport.Y);
            Globals.Instance.TileEngine.MapOriginPosition.X += MathHelper.Max(
                (Globals.Instance.TileEngine.Viewport.X + Globals.Instance.TileEngine.Viewport.Width) -
                (Globals.Instance.TileEngine.MapOriginPosition.X + Globals.Instance.TileEngine.Map.MapDimensions.X * Globals.Instance.TileEngine.Map.TileSize.X), 0f);
            Globals.Instance.TileEngine.MapOriginPosition.Y += MathHelper.Max(
                (Globals.Instance.TileEngine.Viewport.Y + Globals.Instance.TileEngine.Viewport.Height) -
                (Globals.Instance.TileEngine.MapOriginPosition.Y + Globals.Instance.TileEngine.Map.MapDimensions.Y * Globals.Instance.TileEngine.Map.TileSize.Y), 0f);
            if (System.Windows.Forms.Form.ActiveForm ==
    (System.Windows.Forms.Control.FromHandle(Window.Handle) as System.Windows.Forms.Form) &&this.IsActive && input.CurrentMouseStates.X > 0 && input.CurrentMouseStates.X < Globals.Instance.TileEngine.Viewport.Width &&
                input.CurrentMouseStates.Y > 0 && input.CurrentMouseStates.Y < Globals.Instance.TileEngine.Viewport.Height)
            {
                if (input.CurrentMouseStates.RightButton == ButtonState.Pressed)
                {
                    int x = (int)((input.CurrentMouseStates.X - Globals.Instance.TileEngine.MapOriginPosition.X) / Globals.Instance.TileEngine.Map.TileSize.X);
                    int y = (int)((input.CurrentMouseStates.Y - Globals.Instance.TileEngine.MapOriginPosition.Y) / Globals.Instance.TileEngine.Map.TileSize.Y);
                    switch (Globals.Instance.MapEditor.DrawLayer.Text)
                    {
                        case "Layer1":
                            Globals.Instance.TileEngine.Map.BaseLayer[x + y * Globals.Instance.TileEngine.Map.MapDimensions.X] = -1;
                            break;
                        case "Layer2":
                            Globals.Instance.TileEngine.Map.FringeLayer[x + y * Globals.Instance.TileEngine.Map.MapDimensions.X] = -1;
                            break;
                        case "Layer3":
                            Globals.Instance.TileEngine.Map.ObjectLayer[x + y * Globals.Instance.TileEngine.Map.MapDimensions.X] = -1;
                            break;
                    }
                }
                if (input.CurrentMouseStates.LeftButton == ButtonState.Pressed)
                {
                    int x = (int)((input.CurrentMouseStates.X - Globals.Instance.TileEngine.MapOriginPosition.X) / Globals.Instance.TileEngine.Map.TileSize.X);
                    int y = (int)((input.CurrentMouseStates.Y - Globals.Instance.TileEngine.MapOriginPosition.Y) / Globals.Instance.TileEngine.Map.TileSize.Y);
                    switch (Globals.Instance.MapEditor.DrawLayer.Text)
                    {
                        case "Layer1":
                            Globals.Instance.TileEngine.Map.BaseLayer[x + y * Globals.Instance.TileEngine.Map.MapDimensions.X] = Globals.Instance.SelectedTile;
                            break;
                        case "Layer2":
                            Globals.Instance.TileEngine.Map.FringeLayer[x + y * Globals.Instance.TileEngine.Map.MapDimensions.X] = Globals.Instance.SelectedTile;
                            break;
                        case "Layer3":
                            Globals.Instance.TileEngine.Map.ObjectLayer[x + y * Globals.Instance.TileEngine.Map.MapDimensions.X] = Globals.Instance.SelectedTile;
                            break;
                    }
                }
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Globals.Instance.TileEngine.DrawLayers(Globals.Instance.MapEditor.DrawLayer1.Checked,
                Globals.Instance.MapEditor.DrawLayer2.Checked, Globals.Instance.MapEditor.DrawLayer3.Checked);

            base.Draw(gameTime);
        }
    }
}
