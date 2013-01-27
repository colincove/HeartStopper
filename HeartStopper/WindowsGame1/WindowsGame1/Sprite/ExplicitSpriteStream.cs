using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Animation;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Sprite
{
    public class ExplicitSpriteStream:LoopableSpriteStream
    {
        private Rectangle[] rects;

        public ExplicitSpriteStream(Rectangle[] rects, Boolean doesLoop)
               : base(rects.Length, doesLoop)
        {
            this.rects = rects;
        }
        public override Rectangle popRect()
        {
            base.popRect();
            return rects[this.frame-1];
        }
    }
}
