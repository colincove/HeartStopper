using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeartStopper;
using WindowsGame1.Animation;
using WindowsGame1.Sprite;

namespace WindowsGame1
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerSkin : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public LinearSprite sprite;
        private Rectangle destRect;
       public SpriteStreamBase run, walk, jog,idle;
        private Werewolf wolf;
        private double angle;
        private float oldX, oldY;
        public PlayerSkin(Game game, Werewolf wolf)
            : base(game)
        {
            game.Components.Add(this);
            this.wolf = wolf;
            DrawOrder = 5;
            oldX = wolf.X;
            oldY = wolf.Y;

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {

      
            base.Draw(gameTime);
            Map map=((Game1)Game).map;
            int x = (int)(wolf.x / (float)24);
            int y = (int)(wolf.y / (float)24);
            if (x < 0)
            {
                x = 0;
            }
            else if ((int)x > map.getWidth())
            {
                x = map.getWidth();
            }
            if (y < 0)
            {
                y = 0;
            }
            else if (y > map.getHeight())
            {
                y = map.getHeight();
            }
           // float scale=(map.grid[x,y]/(float)6);
           
            sprite.drawSprite(SpriteEffects.None, destRect, gameTime, (float)angle,1.0f);
         
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            Rectangle spriteRect = new Rectangle(0, 0, 36, 74);
            sprite = new LinearSprite(Game.Content.Load<Texture2D>("Images/WolfSprite"), Game1.spriteBatch, spriteRect);
            Rectangle[] rects = new Rectangle[] { new Rectangle(0, 0, spriteRect.Width, spriteRect.Height), new Rectangle(spriteRect.Width, 0, spriteRect.Width, spriteRect.Height), new Rectangle(spriteRect.Width*2, 0, spriteRect.Width, spriteRect.Height) };
            run = new DelayedSpriteStream(new ExplicitSpriteStream(rects, true), 2);
            
            rects = new Rectangle[] { new Rectangle(spriteRect.Width * 3, 0, spriteRect.Width, spriteRect.Height), new Rectangle(spriteRect.Width * 4, 0, spriteRect.Width, spriteRect.Height), new Rectangle(0, spriteRect.Height, spriteRect.Width, spriteRect.Height), new Rectangle(spriteRect.Width, 0, spriteRect.Width, spriteRect.Height) };
         jog = new DelayedSpriteStream(new ExplicitSpriteStream(rects,true),4);
         rects = new Rectangle[] { new Rectangle(spriteRect.Width * 2, spriteRect.Height, spriteRect.Width, spriteRect.Height),
             new Rectangle(spriteRect.Width * 3, 0, spriteRect.Width, spriteRect.Height), 
             new Rectangle(spriteRect.Width * 4, spriteRect.Height, spriteRect.Width, spriteRect.Height),
             new Rectangle(0, spriteRect.Height*2, spriteRect.Width, spriteRect.Height) ,
             new Rectangle(spriteRect.Width, spriteRect.Height*2, spriteRect.Width, spriteRect.Height) ,
             new Rectangle(spriteRect.Width * 2, spriteRect.Height*2, spriteRect.Width, spriteRect.Height) ,
             new Rectangle(spriteRect.Width * 3, spriteRect.Height*2, spriteRect.Width, spriteRect.Height) ,
             new Rectangle(spriteRect.Width * 4, spriteRect.Height*2, spriteRect.Width, spriteRect.Height) 
         };
         walk = new DelayedSpriteStream(new ExplicitSpriteStream(rects, true), 8);
         rects = new Rectangle[] {  new Rectangle(spriteRect.Width * 2, spriteRect.Height, spriteRect.Width, spriteRect.Height)
         };
         idle = new DelayedSpriteStream(new ExplicitSpriteStream(rects, true), 8);
            sprite.updateStream(idle);

            destRect = new Rectangle(0, 0, 36, 72);
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
            destRect.X = (int)(wolf.X-((Game1)Game).cam.X);
            destRect.Y = (int)(wolf.Y-((Game1)Game).cam.Y);
            angle = MathHelper.ToRadians((float)(Math.Atan2((double)oldY - (double)wolf.Y, (double)oldX - (double)wolf.X) / (Math.PI / 180))-90f);
            oldX = wolf.X;
            oldY = wolf.Y;
        }
    }
}
