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
using HeartStopper;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public Map map;

        public Werewolf wW;
        public Sheep sheep;

        //Vision temp;
        public Camera cam;
        public static int VIEWPORT_HEIGHT = 1080;
        public static int VIEWPORT_WIDTH = 1920;

        public const int MAP_SIZE = 129; // must be 2^x + 1, where x is a positive integer

        private HealthBar hb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // View window resolution.
            graphics.PreferredBackBufferHeight = VIEWPORT_HEIGHT;
            graphics.PreferredBackBufferWidth = VIEWPORT_WIDTH;
            Content.RootDirectory = "Content";

            //wW = new Werewolf(this, screenWidth, screenHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            map = new Map(this, MAP_SIZE, MAP_SIZE);
            wW = new Werewolf(this, map.getWidth(), map.getHeight());

            for (int i = 0; i < 50; i++)
            {
                Ball ball = new Ball(this,i);
            }
            //sheep = new Sheep(this, 50, 50, map);

            cam = new Camera(this, wW);
            //temp = new Vision(this, 5, 5);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //hb = new HealthBar(this, 0, 0);
            base.Initialize();
           
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            
           
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

            spriteBatch.Begin();

          
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
