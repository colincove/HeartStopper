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
    public class HealthBar : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private const int WIDTH = 100;
        private const int HEIGHT = 10;

        private Texture2D texture;
        private Color colour;

        private float hp;

        public int x;
        public int y;

        private Game1 game;

        public HealthBar(Game1 game, int x, int y)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.x = x;
            this.y = y;
            DrawOrder = 2000;
            this.hp = 1.0f;
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
            colour = new Color(255, 0, 0, 255);
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new[] { colour });

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            hp -= gameTime.ElapsedGameTime.Seconds / 30;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //Game1.spriteBatch.Begin();

            Game1.spriteBatch.Draw(texture, new Rectangle(x - (int)((Game1)Game).cam.X, y - (int)((Game1)Game).cam.Y, ((int) hp * WIDTH), WIDTH ), new Color(255, 255, 255, 150));

            //Game1.spriteBatch.End();
        }
    }
}
