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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class VisionHighlight : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Game game;
        private Texture2D texture;
        private Color colour;
        private int x; // Positions within the map grid.
        private int y;
        private int id;

        public bool drawIt; 

        public VisionHighlight(Game game, int x, int y, int id)
            : base(game)
        {
            // TODO: Construct any child components here
            DrawOrder = 6;
            this.game = game;
            this.x = x;
            this.y = y;
            this.id = id;
            this.drawIt = false;
            this.game.Components.Add(this);
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
            colour = new Color(id, id, id, id); // White Highlight
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new[] { colour });

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            Game1.spriteBatch.Dispose();
            // If you are creating your texture (instead of loading it with
            // Content.Load) then you must Dispose of it
            texture.Dispose();
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
            if (!drawIt)
                return;
            base.Draw(gameTime);

            

            Game1.spriteBatch.Begin();

            Game1.spriteBatch.Draw(texture, new Rectangle(x * Tile.TILE_SIZE - (int)((Game1)Game).cam.X, y * Tile.TILE_SIZE - (int)((Game1)Game).cam.Y, Tile.TILE_SIZE, Tile.TILE_SIZE), new Color(255,255,255,0));

            Game1.spriteBatch.End();
        }

        public Vector2 getPosition()
        {
            return new Vector2(this.x, this.y);
        }

        public void setPosition(Vector2 pos)
        {
            this.x = (int) pos.X;
            this.y = (int) pos.Y;
        }
    }
}