using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
    public class LinearSprite:Sprite
    {
        //if the entire sprite sheet only uses a single size for all frames, then store that value so we dont have to enter it in multiple times.
        private Rectangle spriteRect;
        public LinearSprite(Texture2D spriteSheet, SpriteBatch batch, Rectangle spriteRect)
            :base(spriteSheet, batch)
        {
            this.spriteRect = spriteRect;
        }
        public  SpriteStreamBase getNewStream(String type, int totalFrames,Vector2 origin, Boolean doesLoop)
        {
            //TODO make a switch statement to look through all types of streams and return the correct one. 
            return base.getNewStream(type, totalFrames, spriteRect,origin, doesLoop);
        }
    }
}
