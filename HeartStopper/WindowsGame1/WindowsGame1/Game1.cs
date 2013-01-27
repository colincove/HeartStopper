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

        // Heart Stopper
        Texture2D tex;
        Rectangle rec;

        // Screen Boundaries
        int screenWidth;
        int screenHeight;

        float moveX, moveY;
        float velocity;
        
        public Map map;

        int mapX, mapY;

        DummyVision temp;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // View window resolution.
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
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
            map = new Map(this, 40, 40);
            temp = new DummyVision(this, 5, 5);
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
            tex = Content.Load<Texture2D>("Images/werewolf");
            rec = new Rectangle(150, 200, 38, 50);

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            // generate map

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

            // werewolf movements -- keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                rec.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                rec.X += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                rec.Y -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                rec.Y += 10;

            // werewolf movements 
            //gamepad
            /*if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                rec.X -= 10;
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                rec.X += 10;
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                rec.Y -= 10;
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                rec.Y += 10;*/
            // thumbsticks
            /*if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                rec.X -= 10;
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                rec.X += 10;
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                rec.Y -= 10;
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                rec.Y += 10;
            */
            moveX += Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X)*(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * 5;
            moveY -= Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y)*(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * 5;

            //moveX += (float)Math.Pow((GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X)*3,3);
            //moveY += (float)Math.Pow((GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y)*-3,3);
            /*if(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y ==0 || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 0)
                velocity = 0;
            if(Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) > 0 || Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) > 0)
                velocity++;
            */
            //moveX += velocity*(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X);
            //moveY += velocity*(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y);
    
            rec.X = (int)moveX;
            rec.Y = (int)moveY;
            //Console.WriteLine(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X);

            // screen boundaries
            if (rec.X <= 0)
            {
                //GamePad.SetVibration(PlayerIndex.One, 1, 1); // max vibration
                rec.X = 0;
            }
            if (rec.X + tex.Width >= screenWidth)
                rec.X = screenWidth - tex.Width;
            if (rec.Y <= 0)
                rec.Y = 0;
            if (rec.Y + tex.Height >= screenHeight)
                rec.Y = screenHeight - tex.Height;


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
            //map = new Map(this, 21, 11);
            spriteBatch.Draw(tex, rec, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
