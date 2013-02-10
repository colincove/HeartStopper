using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HeartStopper;
using Microsoft.Xna.Framework.Input;

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
        public static int sheepListCount = 0;
        private int index = 0;

        static DateTime startRumble;

        private bool alive;
        private bool alert;
        private bool done;


        private const int MIN_MOVE_TIME = 1000;
        private const int MAX_MOVE_TIME = 5000;

        private const double MOVE_PROB = 0.64;
        private const double TURN_PROB = 0.24;
        private const double IDLE_PROB = 0.16;

        private const double UP_PROB = 0.16;
        private const double FLAT_PROB = 0.24;
        private const double DOWN_PROB = 0.6;

        private const int NORTH = 0;
        private const int EAST = 1;
        private const int SOUTH = 2;
        private const int WEST = 3;

        private double PERCENT_CORNER = 0.10;

        private int direction;
        private int lastUpdateTime;
        private Random random;

        private int maxX;
        private int maxY;

        private Map map;


        private Texture2D tex;
        private double changeStateTimer = 0;
        private double lastUpdate = 0;
 
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
            Random random = new Random((int)x);
            game.Components.Add(this);
            xVel = (float)random.Next(0, 5);
            random = new Random((int)y);
            yVel = (float)random.Next(0, 5);

            for (int i = 0; i < stateWeights.GetLength(0); i++)
            {
                totalWeights += stateWeights[i, 0];
            }
            alive = true;
            alert = false;
            done = true; 

            this.map = map;
            
            lastUpdateTime = System.Environment.TickCount;
            random = new Random();
            double randomDirection = random.NextDouble() * 4;// get a random number between 1 and 4

            direction = (int)randomDirection;
         
        }
        public void runAway(){
           
            float angle = (float)Math.Atan2(y - ((Game1)Game).wW.y, x - ((Game1)Game).wW.x);
            xVel += (float)Math.Cos(angle - 1)*(float)1.1;
            yVel += (float)Math.Sin(angle - 1)*(float)1.1;
            if (xVel > 5)
            {
                xVel = 5;
            }
            if (xVel <- 5)
            {
                xVel = -5;
            }
            if (yVel > 5)
            {
                yVel = 5;
            }
            if (yVel < -5)
            {
                yVel = -5;
            }
        }
        public int getX()
        {
            return (int)base.x;
        }

        public int getY()
        {

            return (int)base.y;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            xVel = xVel / 1.001f;
            yVel = yVel / 1.001f;
            x += (int)xVel;
            y += (int)yVel;
            if (!alive && !alert)
            {
                 
                GamePad.SetVibration(PlayerIndex.One,1f,0f);
       
                startRumble = DateTime.Now;
                alert = true;
                
            }
            TimeSpan timePassed = DateTime.Now - startRumble;
            if (!alive && done && timePassed.TotalSeconds >= 0.25)
            {
      
                GamePad.SetVibration(PlayerIndex.One, 0f, .5f);

                alive = false;
                //done = false;
            }
            timePassed = DateTime.Now - startRumble;
            if (!alive && done && timePassed.TotalSeconds >= 0.50)
            {
               
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);

                alive = false;
                done = false;
            }

            base.Update(gameTime);

        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
            if(alive)
            Game1.spriteBatch.Draw(tex, new Rectangle((int)(x - ((Game1)Game).cam.X), (int)(y - ((Game1)Game).cam.Y), 38, 50), Color.White);
            
            
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            tex = Game.Content.Load<Texture2D>("Images/Sheepl");
        }
        public void isAlive(bool state)
        {
            alive = state;
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
    
    }
}
