using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartStopper;

namespace WindowsGame1
{
    public class DepthComponent : DrawableSprite
    {
     
        public float velX = 0.0f;
       public float velY = 0.0f;
        public DepthComponent(Game1 game)
            : base(game)
        {
           
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            x += velX/3;
            y += velY/3;

        }
   
        public float X
        {
            get { return x; }
        }
        public float Y
        {
            get { return y; }
        }
    }
}
