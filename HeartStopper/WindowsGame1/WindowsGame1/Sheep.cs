using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    public class Sheep : DrawableSprite
    {
        public static Sheep[] sheep = new Sheep[100];
        public static float velXAvg = 0.0f;
        public static float velYAvg = 0.0f;
        public static float prevVelXAvg = 0.0f;
        public static float prevVelYAvg = 0.0f;
        public static float posXAvg = 0.0f;
        public static float posYAvg = 0.0f;
        public static float prevPosXAvg = 0.0f;
        public static float prevPosYAvg = 0.0f;
        private static int updateCount = 0;
        public int sheepListCount = 0;
        private int index = 0;
        private Texture2D tex;
        private double changeStateTimer = 0;
        private double lastUpdate = 0;
        private Random random;
        //states
        private int IdleState = 0, MOVE = 1, MOVE_SHEEP = 2, TURN = 3, FLOCK = 4;
        //4 indices match with the four states. 
        private int[,] stateWeights;
        private int currentState;
        private int totalWeights = 0;
        private bool inChase = false;
        private float xVel = 0f, yVel = 0f;
        public Sheep(Game1 game, float x, float y)
            : base(game)
        {
            // Add to the sheep list 
            sheep[sheepListCount] = this;
            index = sheepListCount;
            sheepListCount++;

            stateWeights = new int[,] { { 4, IdleState }, { 10, MOVE }, { 10, MOVE_SHEEP }, { 7, TURN }, { 7, FLOCK } };
            currentState = IdleState;
            base.x = x;
            base.y = y;
            this.DrawOrder = 1000;
            x += 500;
            y += 500;
            random = new Random((int)x);
            game.Components.Add(this);
            xVel = (float)random.Next(0, 5);
            random = new Random((int)y);
            yVel = (float)random.Next(0, 5);
            Console.WriteLine("X: " + xVel + " y: " + yVel);
            for (int i = 0; i < stateWeights.GetLength(0); i++)
            {
                totalWeights += stateWeights[i, 0];
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentState == FLOCK)
            {
                // Add sheep velocity and position to flock average
                velXAvg += xVel;
                velYAvg += yVel;

                posXAvg += x;
                posYAvg += y;

                // Adjust sheep velocity to be closer to the flock average
                float flockDist = prevPosXAvg - x;
                if (flockDist < 10)
                {
                    xVel = (2.0f * xVel + prevVelXAvg) / 3.0f;
                }
                else
                {
                    xVel = (1.0f * xVel + 1.0f * prevVelXAvg + 2.0f * (prevPosXAvg - x)) / 4.0f;
                }

                flockDist = prevPosYAvg - y;
                if (flockDist < 10)
                {
                    yVel = (2.0f * yVel + prevVelYAvg) / 3.0f;
                }
                else
                {
                    yVel = (1.0f * yVel + 1.0f * prevVelYAvg + 2.0f * (prevPosYAvg - y)) / 4.0f;
                }

                /*float xAvg = 0f;
                float yAvg = 0f;
                for (int i = 0; i < sheepListCount; i++)
                {
                    xAvg += sheep[i].xVel;
                    yAvg += sheep[i].yVel;
                }*/

                updateCount++;

                if (updateCount == sheepListCount)
                {
                    prevVelXAvg = velXAvg / (float)sheepListCount;
                    prevVelYAvg = velYAvg / (float)sheepListCount;

                    prevPosXAvg = posXAvg / (float)sheepListCount;
                    prevPosYAvg = posYAvg / (float)sheepListCount;

                    velXAvg = 0.0f;
                    velYAvg = 0.0f;
                    posXAvg = 0.0f;
                    posYAvg = 0.0f;
                }
            }
            if (!inChase)
            {
                lastUpdate += gameTime.ElapsedGameTime.Milliseconds;
                if (lastUpdate > changeStateTimer)
                {
                    lastUpdate = 0;
                    changeStateTimer = (random.Next(1000));
                    int randomState = random.Next(totalWeights);
                    int counter = 0;
                    for (int i = 0; i < stateWeights.GetLength(0); i++)
                    {
                        counter += stateWeights[i, 0];
                        if (counter > randomState)
                        {
                            currentState = stateWeights[i, 1];
                            break;
                        }
                    }
                    currentState = FLOCK;
                    if (currentState == IdleState)
                    {
                        xVel = 0;
                        yVel = 0;
                    }
                    else if (currentState == MOVE)
                    {
                        xVel = ((float)random.Next(100) / 100 * 2 - 1);
                        yVel = ((float)random.Next(100) / 100 * 2 - 1);
                    }
                    else if (currentState == MOVE_SHEEP)
                    {
                        xVel = ((float)random.Next(100) / 100 * 2 - 1);
                        yVel = ((float)random.Next(100) / 100 * 2 - 1);
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
            Game1.spriteBatch.Draw(tex, new Rectangle((int)(x - ((Game1)Game).cam.X), (int)(y - ((Game1)Game).cam.Y), 38, 50), new Rectangle(0, 0, 38, 50), Color.White);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            tex = Game.Content.Load<Texture2D>("Images/werewolf");
        }

    }
}
