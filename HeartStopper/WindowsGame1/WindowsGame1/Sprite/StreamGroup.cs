using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
    //will play a group of animations. 
    class StreamGroup:LoopableSpriteStream
    {
        //the array of streams to be displayed. 
        private SpriteStreamBase[] streams;
        //stream that is currently being displayed. This is the stream that is asked for a rectangle during popRect
        private SpriteStreamBase currentStream;
        //we are given an array of streams to display. currentStreamIndex points to the location of the currentStream in the array. Will be used to switch to the next stream when the time comes. 
        private int currentStreamIndex = 0;
        public StreamGroup(SpriteStreamBase[] streams, Boolean doesLoop)
            : base(StreamGroup.getFrameCount(streams),doesLoop)
        {
            currentStream = streams[0];
            this.streams = streams;
        }
        public StreamGroup(SpriteStreamBase[] streams)
            : this(streams, false)
        {
   
        }
        //StreamGroup still needs a frame count for its animation.
        public static int getFrameCount(SpriteStreamBase[] streams)
        {
            int frameCount = 0;
            for (int i = 0; i < streams.Length; i++)
            {
                frameCount += streams[i].totalFrames;
            }
            return frameCount;
        }
        public override Rectangle popRect()
        {
            //if the current stream has reached its final frame, we need to get the next animation in the group or loop the group itself. 
            if (currentStream.streamComplete)
            {
                //if there is still another animation left, go get it. 
                if (currentStreamIndex < streams.Length - 1)
                {
                    //by resetting the current stream, when we loop back to it (if this object does loop that is), it will play from frame 1. 
                    //if we do not reset it, it would already be fully animated next time we attempt to play it
                    currentStream.resetStream();
                    //incremenet currentStream index and get the next animation in the group. 
                    currentStream = streams[++currentStreamIndex];
                }
                    //this check on does loop may not be needed if we fix the frame increment isdues listed below. For we, we have to handle looping if it exists. 
                else if(doesLoop)
                {
                    resetStream();
                }
            }
            //TODO: because of objects like DelayedSpriteStream, its possible base.popRect() here will increment the frame when in reality, it should stay as was,
            //until DelayedSpriteStream object truly animates a new frame of animation. 
            base.popRect();
            //return the stream of the current animation of the group.
            return currentStream.popRect();
        }

        public override void resetStream()
        {
            currentStream.resetStream();
            currentStream = streams[0];
            currentStreamIndex = 0;
            base.resetStream();

        }
    }
}
