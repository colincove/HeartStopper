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
using WindowsGame1;

namespace HeartStopper
{
    public class Werewolf : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D tex;
        public Rectangle rec;
        float moveX, moveY;
        private int screenWidth;
        private int screenHeight;

        public Werewolf(Game game, int width, int height)
            : base(game)
        {
            // TODO: Construct any child components here
            DrawOrder = 5; // Always draw this last.
            screenWidth = width*Tile.TILE_SIZE;
            screenHeight = height*Tile.TILE_SIZE;
            game.Components.Add(this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            
            base.Initialize();


        }

        protected override void LoadContent()
        {
            base.LoadContent();
            //this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            // For now texture is coloured rectangle.
            tex = Game.Content.Load<Texture2D>("Images/werewolf");
            rec = new Rectangle(150, 200, 38, 50);
            //float g = ((float)elevation / (float)MAX_ELEVATION) * 255f;
            //dummyColor = new Color(0, (int)g, 0);
            //texture = new Texture2D(game.GraphicsDevice, 1, 1);
            //texture.SetData(new[] { dummyColor });
            

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Console.WriteLine(rec.X + tex.Width);
            doAccMovement(gameTime);
            addRestrictions(gameTime);

        }
        private void addRestrictions(GameTime gameTime)
        {
            // set boundaries
            //GamePad.SetVibration(PlayerIndex.One, 1, 1); // max vibration
            if (rec.X <= 0)
                rec.X = 0;
            if (rec.X + tex.Width >= screenWidth)
                rec.X = screenWidth - tex.Width;
            if (rec.Y <= 0)
                rec.Y = 0;
            if (rec.Y + tex.Height >= screenHeight)
                rec.Y = screenHeight - tex.Height;
            
            base.Update(gameTime);
        }
        private void doMovement(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                rec.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                rec.X += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                rec.Y -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                rec.Y += 10;

            moveX += Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * 5;
            moveY -= Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * 5;

            rec.X = (int)moveX;
            rec.Y = (int)moveY;
            base.Update(gameTime);
        }
        private float velX = 0.0f;
        private float velY = 0.0f;
        private void doAccMovement(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 0.0)
            {
                velX = velX / 1.1f;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 0.0)
            {
                velY = velY / 1.1f;
            }
            velX += Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * 1;
            velY -= Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * 1;

            rec.X += (int)velX;
            rec.Y += (int)velY;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(tex, rec, Color.White);
            Game1.spriteBatch.End();
        }


      
    }
}
