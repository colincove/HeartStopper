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


namespace WindowsGame1
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class DrawableSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public float x;
        public float y;

        public float xp;
        public float yp;

        public float angle;

        public DrawableSprite(Game game)
            : base(game)
        {
            x = 0;
            y = 0;
            xp = 0;
            yp = 0;
            // TODO: Construct any child components here
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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //angle = MathHelper.ToRadians((float)(Math.Atan2((double)yp - (double)y, (double)xp - (double)x) / (Math.PI / 180)) - 90f);

            angle = VectorToAngle(new Vector2(x - xp, y - yp));
            //Console.WriteLine(angle);
            xp = x;
            yp = y;
            base.Update(gameTime);
        }

        public Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        public float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }
    }
}
