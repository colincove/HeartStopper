using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeartStopper;
using WindowsGame1;

namespace HeartStopper
{
    public class Werewolf : DrawableSprite
    {
        float moveX, moveY;//, x, y;
        private int oldDepth = 0;
        private float depthFriction = 0.0f;
        private int screenWidth;
        private int screenHeight;

        static DateTime startRumble;
        bool run = true;
        bool hit = false;

        Game1 game;
        Vision vis;

        private PlayerSkin skin;

        public Werewolf(Game1 game, int width, int height)
            : base(game)
        {
            // TODO: Construct any child components here
            DrawOrder = 1000; // Always draw this last.
            screenWidth = width*Map.TILE_SIZE;
            screenHeight = height*Map.TILE_SIZE;
            game.Components.Add(this);
            skin = new PlayerSkin(game, this);
            this.x = 0;
            this.y = 0;
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            vis = new Vision(game, this);
            
            base.Initialize();


        }

        protected override void LoadContent()
        {
            base.LoadContent();
            

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //doMovement(gameTime);
           // doAccMovement(gameTime);
            Map map=((Game1)Game).map;
            //int currentDepth = map.grid[(int)(x * Map.TILE_SIZE),(int)(y * Map.TILE_SIZE)];
           // depthFriction += oldDepth - currentDepth;
            //oldDepth = currentDepth;
            doKeyAccMovement(gameTime);
            //addRestrictions(gameTime);
            //base.Update(gameTime);

        }
        private void addRestrictions(GameTime gameTime)
        {
            
            //int milliseconds = 2000;
            //Thread.Sleep(milliseconds); 
            //startRumble = DateTime.Now;
            // set boundaries
            //GamePad.SetVibration(PlayerIndex.One, 1, 1); // max vibration

            
            if (x <=0 && !hit)
            {
                //GamePad.SetVibration(PlayerIndex.One,0.3f,0.3f);
                hit = true;
                
                //Thread.Sleep(milliseconds);
                if (hit&&run)
                {
                    Console.WriteLine("1");
                    GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
                    startRumble = DateTime.Now;
                    run = false;
                }
                //GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                 
            }

           



            /*if (hit&&run)
            {
                Console.WriteLine("1");
                //GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
                startRumble = DateTime.Now;
                run = false;
                
            }*/

            TimeSpan timePassed = DateTime.Now - startRumble;
            if (!run && timePassed.TotalSeconds >= 1.0)
            {
                //hit = false;
                Console.WriteLine("2");
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                run = true;
                //x = 0; 
            }
            
            //if (x > 1) 
            //    hit = false;
        }
        private void doMovement(GameTime gameTime)
        {
            bool isRunning = false;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                moveX -= 10;
                isRunning=true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                moveX += 10;
                isRunning = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                moveY -= 10;
                isRunning = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                moveY += 10;
                isRunning = true;
            }
            
            moveX += Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * 1;
            moveY -= Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * 1;
            changeAnimation(isRunning);
            x = moveX;
            y = moveY;
            base.Update(gameTime);
        }
        private float velX = 0.0f;
        private float velY = 0.0f;
        private void doAccMovement(GameTime gameTime)
        {

            bool isRunning = false;
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 0.0)
            {
                velX = velX / 1.3f;
            }
            else
            {
                isRunning = true;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 0.0)
            {
                velY = velY / 1.3f;
            }
            else
            {
                isRunning = true;
            }
            changeAnimation(isRunning);
           velX += Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * 1;
            velY -= Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * 1;

            x += velX;
            y += velY;
        }
        private void doKeyAccMovement(GameTime gameTime)
        {
            bool isRunning = false;
            bool hRun = false;
            bool vRun = false;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velX -= .5f;
                isRunning = true;
                hRun = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velX += .5f;
                isRunning = true;
                hRun = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velY -= .5f;
                isRunning = true;
                vRun = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velY += .5f;
                isRunning = true;
                vRun = true;
            }

            if (!vRun)
            {
                velY = velY / 1.2f;
            }
            else
            {
                velY = velY / 1.05f;
            }
            if (!hRun)
            {
                velX = velX / 1.2f;
            }
            else
            {
                velX = velX / 1.05f;
            }
            changeAnimation(isRunning);
            // velX += Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * 1;
            // velY -= Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * 1;

            x += velX;
            y += velY;
            base.Update(gameTime);
        }
        private void changeAnimation(bool isRunning)
        {
            if (isRunning)
            {
                if (Math.Abs(velX)  + Math.Abs(velY) <3)
                {
                    skin.sprite.updateStream(skin.walk);
                }
                else if (Math.Abs(velX) + Math.Abs(velY) < 6)
                {
                    skin.sprite.updateStream(skin.jog);
                }
                else 
                {
                    skin.sprite.updateStream(skin.run);
                }
            }
            else
            {
                skin.sprite.updateStream(skin.idle);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public float X
        {
            get { return x; }
        }
        public float Y
        {
            get { return y; }
        }
      
    }
}
