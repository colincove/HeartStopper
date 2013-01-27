using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
//LoopableSpriteStream.cs Colin Cove 2012
/*
Upon poping the source rectangle, the frame is incremented by SpriteStreamBase. A loopable sprite will reset the frame if it happens to reach the end of its animation.  
*/
   public class LoopableSpriteStream : SpriteStreamBase
    {
	//This class defines an object that can be looped, but this variable defines whether or not it is loopable or not. 
        protected Boolean doesLoop;
        public LoopableSpriteStream(int totalFrames, Boolean doesLoop)
               :base(totalFrames)
        {
            this.doesLoop = doesLoop;
        }
        public LoopableSpriteStream(int totalFrames)
                 : this(totalFrames, true)
             {  
             }
             public override Rectangle popRect()
             {
                
				 //reset the frame count if it reaches the end of its frame count. Only IF it is loopable. 
                 if (streamComplete && doesLoop)
                 {
                    
                     _frame = 0;
                 } 
                 Rectangle returnRect = base.popRect();
                 return returnRect;
             }
    }
}
