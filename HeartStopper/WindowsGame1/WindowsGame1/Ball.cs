using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1.Sprite;

namespace WindowsGame1
{
    public class Ball : SlopedComponent
    {
        public LinearSprite sprite;
        private Rectangle destRect;
        public SpriteStreamBase idle;
        public Ball(Game1 game, int seed)
            : base(game)
        {
            destRect = new Rectangle(0,0,36,36);
            x = 1500;
            y = 1500;
            
           
            game.Components.Add(this);
            Random random = new Random(seed);
            x = (float)random.Next(0,3000);
           random = new Random((int)(seed*3.2));
            y = (float)random.Next(0, 3000);
            damp = 4.0f;
            applyForce = true;
            slope.range = 6;
      
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            sprite.drawSprite(SpriteEffects.None, destRect, gameTime, 0f, 1.0f);
        }
           protected override void LoadContent()
        {
            base.LoadContent();

            Rectangle spriteRect = new Rectangle(0, 0, 36, 36);
            sprite = new LinearSprite(Game.Content.Load<Texture2D>("Images/Ball"), Game1.spriteBatch, spriteRect);
            Rectangle[] rects = new Rectangle[] { new Rectangle(0, 0, spriteRect.Width, spriteRect.Height) };
            idle = new DelayedSpriteStream(new ExplicitSpriteStream(rects, true), 1);

            sprite.updateStream(idle);
         }
           public override void Update(GameTime gameTime)
           {
               
               destRect.X = (int)(X - ((Game1)Game).cam.X);
               destRect.Y = (int)(Y - ((Game1)Game).cam.Y);
               velX = velX / 1.03f;
               velY = velY / 1.03f;

               float xDist= x - ((Game1)Game).wW.x;
               float yDist=y - ((Game1)Game).wW.y;
               float dist = (float)Math.Sqrt(xDist * xDist+ yDist * yDist);
               if (dist < 22)
               {
                  /// float angle = (float)Math.Atan2(yDist,xDist);
              // velX += (float)Math.Cos(angle - 1) * (float)5;
              // velY += (float)Math.Sin(angle - 1) * (float)5;
                   velY += ((Game1)Game).wW.velY;
                   velX += ((Game1)Game).wW.velX;
               }
               base.Update(gameTime);
           }
        
    }
}
