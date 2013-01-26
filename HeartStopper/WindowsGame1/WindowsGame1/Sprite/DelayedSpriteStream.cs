using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
    //DelayedSpriteStream Colin Cove 2012
    /*
      This class acts as a wrapper for stream objects. It will delay the popRect of its stream object for as many frames as the delay is set.
      To create an animation made up of a variable number of delayed frame animations, you would create a StreamGroup filled with DelayedSpriteStream objects. 
     * */
    class DelayedSpriteStream:SpriteStreamBase
    {
        //internal stream with which this class wraps around. 
        private SpriteStreamBase stream;
        //the number of frames we want to delay the animation. 
        private int frameDelay;
        //current number of frame that have elapsed since the last new frame animation. 
        private int frameElapse;
        /*
          while delaying the new frame, this class will just output the last known frame and wait until 
          the delay is complete before swapping out current frame with the next frame of animation. 
         */
        private Rectangle currentRect;
        
        public DelayedSpriteStream(SpriteStreamBase stream, int frameDelay)
            : base (0)
        {
            this.stream = stream;
            this.frameDelay=frameDelay;
            frameElapse = frameDelay;
        }
        public override int totalFrames
        {
            get { return stream.totalFrames; }
        }
        public override  int frame
        {
            get { return stream.frame; }
        }
        public override Boolean streamComplete
        {
            //normally I would get stream complete from the stream object (being a wrapper and all), 

            //but we should read as complete after a full delay even while at the last frame. 
            
            get { return _streamComplete; }
        }
        public override Rectangle popRect()
        {
            //increment the elapse count, and if it has hit the delay, then provide a new frame of animation.
            //popRect is called according to frameRate (by a Sprite object), so frameDelay is how many frames of delay you want. 
            if (++frameElapse >= frameDelay)
            { 
                //without setting streamComplete after a full delay, the last frame of a DelayedSpriteStream would only render once, as opposed to according to the delay. 
                if (stream.streamComplete)
                {
                    _streamComplete = true;
                }
                currentRect = stream.popRect();
                frameElapse = 0;
               
            }
            
            return currentRect;
        }
        public override void resetStream()
        {
            _streamComplete = false;
            stream.resetStream();
        } 

    }
}
