using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
//SpriteStreamBase Colin Cove 2012
/*
This class acts as the base for all streaming animations used in the Animation framework. 
Streaming objects provide Sprite objects will source rectangles. 
Each type of streaming object will define how these source objects are provided. 
*/
    public class SpriteStreamBase
    {
//the cirrent frame of the animation. Sub classes will use this number to calculate where to get its source Rectangle. 
        protected int _frame = 1;
		//total frames of the animation. 
            private int _totalFrames;
        //when the animation has reached the end of its frame count, stream complete will be true. 
        //This can notify a StreamGroup object to move onto the next frame of animation. 
            protected Boolean _streamComplete;

        public SpriteStreamBase(int totalFrames)
        {
            _totalFrames = totalFrames;
     
        }
		//This method is called by a Sprite object. The term 'pop' is used because it will increment the frame each time, and will return the current frame's Rectangle. 
		//If called again, because the frame was incremented, it will recieve the next Rectangle in the animation. 
        public virtual Rectangle popRect()
        {
            if (++_frame < _totalFrames)
            {
                _streamComplete = false;

            }
            else
            {
                _frame = _totalFrames;
                _streamComplete = true;
            }
            return new Rectangle();
        }
        public virtual int totalFrames
        {
            get { return _totalFrames; }
        }
        public virtual int frame
        {
            get { return _frame; }
        }
        public virtual Boolean streamComplete
        {
            get { return _streamComplete; }
        }
        public virtual void resetStream()
        {
            _frame = 1;
            _streamComplete = false;
        }

    }
}
