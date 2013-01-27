using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1.Animation
{
//Sprite Colin Cove 2012
/*
The Sprite class handles the swapping of animations and acts as incapsulation for the sprite sheet and the batch used to draw it. 
*/
    public class Sprite
    {
        public static String LINEAR_SPRITE = "linearSprite";
        
        private Texture2D spriteSheet;
        private SpriteStreamBase currentStream;
        private Rectangle currentRect;
        //Batch objects are used to draw to. 
        private SpriteBatch batch;
        private TimeSpan currentFrameTime;
      
        private int fps = 30;
        public Sprite(Texture2D spriteSheet, SpriteBatch batch)
        {
            this.spriteSheet = spriteSheet;
            this.batch = batch;
        }
		//updateStream swaps out the current animation fot the one defined in the parameter. Next time drawSprite is called, it will be the new sprite animation.
        public void updateStream(SpriteStreamBase newStream)
        {
            //reset old steam so it can be displayed correctly again later
            //upon first setting currentStream, the value would in fact be null;
            //we also allow the same stream to be set, but just dont reset it. FUTURE: reset sprite when set again, make developers be more strict on when they set a new stream
            if (currentStream != newStream && currentStream!=null)
            {
                currentStream.resetStream();
            }
           
            currentStream = newStream;
        }
		//called when an object desires its sprite to be drawn to the screen. 
        private Rectangle sourceRect;
        public void drawSprite(SpriteEffects effects,Rectangle destinationRect, GameTime gameTime, float rotation, float scale)
        {
            //ElapsedGameTime is how long ago Draw was called. We add this elapse time up and see if we should animate a new frame. 
           currentFrameTime= currentFrameTime.Add(gameTime.ElapsedGameTime);
            //1000 milliseconds in a seconds, and fps represents frame per SECOND. So devide by 1000. 
            if (currentFrameTime.Milliseconds > 1000/fps)
            {
                if (currentStream != null)
                {
                    //ofcourse, if enough time has passed, then ask for a new frame of animation from your stream and set the stored rectangle for return in the next Draw
                    sourceRect = currentStream.popRect();
                    currentRect = sourceRect;
                    //reset the frame time to be checked next draw 
                    currentFrameTime = new TimeSpan();
                }
            }
            else
            {
                //if, according to your frame rate, a new frame should NOT be drawn, then send out the old rectangle (redraw that frame). 
                sourceRect = currentRect;
            }
            if (currentRect!=null)
            {
                //do the actual drawing. 
                batch.Draw(spriteSheet, destinationRect, sourceRect, Color.White, rotation, new Vector2((sourceRect.Width / 2) * scale, (sourceRect.Height / 2) *scale), effects, 0.0F);
            }
        }
 //This method should be the only way to get access to Stream objects. Because stream objects have an association with the sheet used to create them, we should not 
 //allow a Sprite object to be updated with a stream object that did not originate from itself. 
        public virtual SpriteStreamBase getNewStream(String type,int totalFrames,Rectangle spriteRect,Vector2 origin, Boolean doesLoop)
        {
            //TODO make a switch statement to look through all types of streams and return the correct one. 
            return new LinearSpriteStream(totalFrames, new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height), spriteRect,origin,doesLoop);
        }
    }
}
