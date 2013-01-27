using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartStopper;

namespace WindowsGame1
{
    public class Camera:DrawableGameComponent
    {
        private Werewolf wolf;
        private float x, y, follow=10f;
        public Camera(Game game, Werewolf wolf) :
            base(game)
        {
            game.Components.Add(this);
            this.wolf = wolf;
        }
        public override void Update(GameTime gameTime)
        {
            x -= (x - wolf.X) / follow;
            y -= (y - wolf.Y) / follow;
            base.Update(gameTime);
        }
        public float X
        {
            get { return x - Game1.VIEWPORT_WIDTH / 2; }
        }
        public float Y
        {
            get { return y - Game1.VIEWPORT_HEIGHT / 2; }
        }
    }
}
