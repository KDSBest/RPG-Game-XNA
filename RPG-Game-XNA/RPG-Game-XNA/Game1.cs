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
using RPG_Game_XNA.GameStateManagement;
using RPG_Game_XNA.GameScreen;

namespace RPG_Game_XNA
{
    /// <summary>
    /// This is the main type for your game
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Initialize screen size to an ideal resolution for the XBox 360
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
        }


        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Globals.Instance.Initialize(Content, GraphicsDevice);
            GameStateManager.Instance.AddScreen(new MainMenuScreen(), true, true);
            // TODO: use this.Content to load your game content here
        }


        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            GameStateManager.Instance.Update(gameTime);
            if (GameStateManager.Instance.Exit)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// This is called when the game should draw itself.
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GameStateManager.Instance.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
