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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Tile : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public const int TILE_SIZE = 36; // pretzels
        public const int MAX_ELEVATION = 10;

        private Game game;
        private int elevation;
        private Texture2D texture;
        private Vector2 id; // x,y index of location in grid.
        private Color dummyColor;

        public Tile(Game game, Vector2 id, int elevation)
            : base(game)
        {
            // TODO: Construct any child components here
            DrawOrder = 1; // Always draw this first.
            this.game = game;
            this.id = id;
            this.elevation = elevation;
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

            float g = ((float)elevation / (float)MAX_ELEVATION) * 255f;
            dummyColor = new Color(0, (int) g, 0);
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new[] { dummyColor });
            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

            Game1.spriteBatch.Begin();

            int x = (int)id.X * TILE_SIZE;
            int y = (int)id.Y * TILE_SIZE;
            Game1.spriteBatch.Draw(texture, new Rectangle(x - (int)((Game1)Game).cam.X, y-(int)((Game1)Game).cam.Y, TILE_SIZE, TILE_SIZE), Color.White);

            Game1.spriteBatch.End();
        }


        public void setElevation(int e)
        {
            this.elevation = e;
        }

        public int getElevation()
        {
            return this.elevation;
        }

    }
}
