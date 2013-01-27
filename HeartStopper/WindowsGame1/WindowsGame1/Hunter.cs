using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public class Hunter:DrawableSprite
    {
        private Texture2D tex;
        private double changeStateTimer = 0;
        private double lastUpdate=0;
        private Random random;
        //states
        private int IdleState = 0, MOVE = 1, MOVE_SHEEP = 2, TURN = 3;
        //4 indices match with the four states. 
        private int[,] stateWeights;
        private int currentState;
        private int totalWeights=0;
        private bool inChase = false;
        private float xVel=0f, yVel=0f;
        public Hunter(Game1 game, float x, float y)
            : base(game)
        {
          
            stateWeights = new int[,] { { 4, IdleState }, { 10, MOVE }, { 10, MOVE_SHEEP }, { 7, TURN } };
            currentState = IdleState;
            base.x =1000;
            base.y = 200;
            this.DrawOrder = 1000;
            random = new Random();
            game.Components.Add(this);
            for (int i = 0; i < stateWeights.GetLength(0); i++)
            {
                totalWeights += stateWeights[i,0];
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (!inChase)
            {
                lastUpdate += gameTime.ElapsedGameTime.Milliseconds;
                if (lastUpdate > changeStateTimer)
                {
                    lastUpdate = 0;
                    changeStateTimer = (random.Next(500));
                    int randomState = random.Next(totalWeights);
                    int counter = 0;
                    for (int i = 0; i < stateWeights.GetLength(0); i++)
                    {
                        counter+=stateWeights[i, 0];
                        if (counter > randomState)
                        {
                            currentState = stateWeights[i, 1];
                            break;
                        }
                    }
                    if (currentState == IdleState)
                    {
                        xVel = 0;
                        yVel = 0;
                    }
                    else if (currentState == MOVE)
                    {
                        xVel = ((float)random.Next(200) / 200-1);
                        yVel = ((float)random.Next(200) / 200-1);
                    }
                    else if (currentState == MOVE_SHEEP)
                    {
                        xVel = ((float)random.Next(200) / 200 - 1);
                        yVel = ((float)random.Next(200) / 200 - 1);
                    }
                    else if (currentState == TURN)
                    {
                        xVel = xVel * -1;
                    }
                }
               
            }
            else
            {

            }
            x += xVel;
            y += yVel;
        }
        private void doIdleState()
        {
        }
        private void doMoveState()
        {
        }
        private void doMoveSheepState()
        {
        }
        private void doTurnState()
        {
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            Game1.spriteBatch.Draw(tex, new Rectangle((int)(x-((Game1)Game).cam.X),(int)(y-((Game1)Game).cam.Y),38,50), new Rectangle(0,0,38,50), Color.White);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            tex = Game.Content.Load<Texture2D>("Images/werewolf");
        }

    }
}
