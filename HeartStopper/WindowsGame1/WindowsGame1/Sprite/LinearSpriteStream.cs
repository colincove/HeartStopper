using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
//LinearSpriteStream Colin Cove 2012
/*
Is a loopable sprite that should move its source rectangle from left to right, dropping down a line when it hits the edge of its bouding box, until it finish's all its frames. 
*/
    class LinearSpriteStream : LoopableSpriteStream
    {
	//represents the top right location of the first frame of animation. 
        protected Vector2 origin;
//spriteSheetBounds will represent the bounding box of the animation used on the sheet. This may just be the entire sheet itself or a small portion of it. 
//spriteRect represents the dimensions of the source rectangle provided by popRect. This will also be used to calculate (along with the bounds) when the source rectangle should drop down to the next line. 
        protected Rectangle spriteSheetBounds,spriteRect;
		//these variables will help speed up the process of calculating the current frame of animation. 
        private int col, row,topRow;

        public LinearSpriteStream(int totalFrames, Rectangle spriteSheetBounds, Rectangle spriteRect, Vector2 origin, Boolean doesLoop)
               :base(totalFrames, doesLoop)
        {
            this.origin = origin;
            this.spriteRect = spriteRect;
            this.spriteSheetBounds = spriteSheetBounds;
            col = spriteSheetBounds.Width / spriteRect.Width;
            row = spriteSheetBounds.Height / spriteRect.Height;
        }
        public LinearSpriteStream(int totalFrames, Rectangle spriteSheetBounds, Rectangle spriteRect, Vector2 origin)
            : this(totalFrames, spriteSheetBounds, spriteRect,origin, false)
           {

           }
        private int rectX, xOffset, yOffset=0;
        public override Rectangle popRect()
        {
            Rectangle returnRect;
            if (frame > col)
            {
                rectX = frame % col;
            }
            else
            {
                rectX = col-(col - frame);
                xOffset = (int)origin.X;
            }
            returnRect = new Rectangle(spriteRect.Width*(rectX-1)+xOffset,
                spriteRect.Height*((int)((frame-1)/col))+spriteSheetBounds.Y,
                spriteRect.Width,
                spriteRect.Height
                );

           base.popRect();
            return returnRect;

        }

    }
}
