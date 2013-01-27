using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeartStopper;
using WindowsGame1;

namespace WindowsGame1
{
    public class Sheep : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int x;
        private int y;

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
        public Sheep(Game1 game, int startX, int startY, Map map)
            : base(game)
        {
            maxX = game.map.grid.GetLength(0);
            maxY = game.map.grid.GetLength(1);
            x = startX;
            y = startY;

            this.map = map;
            
            lastUpdateTime = System.Environment.TickCount;
            random = new Random();
            double randomDirection = random.NextDouble() * 4;// get a random number between 1 and 4

            direction = (int)randomDirection; 
        }

        public int getX()
        {
            updatePosition();
            return x;
        }

        public int getY()
        {
            updatePosition();
            return y;
        }

        private void updatePosition()
        {

            while (lastUpdateTime < System.Environment.TickCount)
            {
                lastUpdateTime = lastUpdateTime + random.Next(MAX_MOVE_TIME - MIN_MOVE_TIME) + MIN_MOVE_TIME;

                double randomAction = random.NextDouble();
                if (randomAction < MOVE_PROB)
                {
                    int newX1=0;
                    int newX2= 0;
                    int newX3= 0;

                    int newY1=0;
                    int newY2=0;
                    int newY3=0;

                    if (direction == NORTH || direction == SOUTH)
                    {
                        newX1 = x - 1;
                        newX2 = x;
                        newX3 = x + 1;
                        if (direction == NORTH)
                        {
                            newY1 = y + 1;
                            newY2 = y + 1;
                            newY3 = y + 1;
                        }
                        else if(direction == SOUTH)
                        {
                            newY1 = y - 1;
                            newY2 = y - 1;
                            newY3 = y - 1;
                        }
                    }

                    if (direction == WEST || direction == EAST)
                    {
                        newY1 = x - 1;
                        newY2 = x;
                        newY3 = x + 1;
                        if (direction == EAST)
                        {
                            newX1 = y + 1;
                            newX2 = y + 1;
                            newX3 = y + 1;
                        }
                        else if (direction == WEST)
                        {
                            newX1 = y - 1;
                            newX2 = y - 1;
                            newX3 = y - 1;
                        }
                    }
                    if (newX1 >= 0
                        && newX2 >= 0
                        && newX3 >= 0
                        && newX1 < maxX
                        && newX2 < maxX
                        && newX3 < maxX
                        && newY1 >= 0
                        && newY2 >= 0
                        && newY3 >= 0
                        && newY1 < maxY
                        && newY2 < maxY
                        && newY3 < maxY)
                    {
                        int elevation1 = map.grid[newX1, newY1];
                        int elevation2 = map.grid[newX2, newY2];
                        int elevation3 = map.grid[newX3, newY3];
                        int currentElevation = map.grid[x, y];
                        /*
                        int elevation1 = map.grid[newX1, newY1].getElevation();
                        int elevation2 = map.grid[newX2, newY2].getElevation();
                        int elevation3 = map.grid[newX3, newY3].getElevation();
                        int currentElevation = map.grid[x, y].getElevation();
                        */

                        double randElevation = random.NextDouble();
                        bool foundTile = true;
                        if (randElevation < UP_PROB)
                        {
                            if (elevation1 > currentElevation)
                            {
                                x = newX1;
                                y = newY1;
                            }
                            else if (elevation2 > currentElevation)
                            {
                                x = newX2;
                                y = newY2;
                            }
                            else if (elevation3 > currentElevation)
                            {
                                x = newX3;
                                y = newY3;
                            }
                            else
                            {
                                foundTile = false;
                            }
                        }
                        else if (randElevation < UP_PROB + FLAT_PROB)
                        {
                            if (elevation1 == currentElevation)
                            {
                                x = newX1;
                                y = newY1;
                            }
                            if (elevation2 == currentElevation)
                            {
                                x = newX2;
                                y = newY2;
                            }
                            if (elevation3 == currentElevation)
                            {
                                x = newX3;
                                y = newY3;
                            }
                            else
                            {
                                foundTile = false;
                            }
                        }
                        else
                        {
                            if (elevation1 < currentElevation)
                            {
                                x = newX1;
                                y = newY1;
                            }
                            else if (elevation2 < currentElevation)
                            {
                                x = newX2;
                                y = newY2;
                            }
                            else if (elevation3 < currentElevation)
                            {
                                x = newX3;
                                y = newY3;
                            }
                            else
                            {
                                foundTile = false;
                            }
                        }
                        if (!foundTile)
                        {
                            double randTile = random.NextDouble();
                            if (randTile < 0.33)
                            {
                                x = newX1;
                                y = newY1;
                            }
                            else if (randTile < 0.66)
                            {
                                x = newX2;
                                y = newY2;
                            }
                            else
                            {
                                x = newX3;
                                y = newY3;
                            }
                        }
                    }
                }
                else if (randomAction < MOVE_PROB + TURN_PROB)
                {
                    //turn
                    int xCellsCorner = (int) PERCENT_CORNER * maxX;
                    int yCellsCorner = (int) PERCENT_CORNER * maxY;
                    bool bottomLeft = x < xCellsCorner && y < yCellsCorner;
                    bool bottomRight = x > maxX-xCellsCorner && y < yCellsCorner;
                    bool topLeft = x < xCellsCorner && y > maxY - yCellsCorner;
                    bool topRight = x > maxX-xCellsCorner && y > maxY - yCellsCorner;
                    double randomTurn = random.NextDouble();
                    if (bottomLeft)
                    {
                        if (randomTurn < 0.5)
                        {
                            direction = NORTH;
                        }
                        else
                        {
                            direction = EAST;
                        }
                    }
                    else if (bottomRight)
                    {
                        if (randomTurn < 0.5)
                        {
                            direction = NORTH;
                        }
                        else
                        {
                            direction = WEST;
                        }
                    }
                    else if (topLeft)
                    {
                        if (randomTurn < 0.5)
                        {
                            direction = SOUTH;
                        }
                        else
                        {
                            direction = EAST;
                        }
                    }
                    else if (topRight)
                    {
                        if (randomTurn < 0.5)
                        {
                            direction = SOUTH;
                        }
                        else
                        {
                            direction = WEST;
                        }
                    }
                    else
                    {
                        
                        if (randomTurn < 0.5)
                        {
                            // turn right
                            if (direction == WEST)
                            {
                                direction = NORTH;
                            }
                            else
                            {
                                direction++;
                            }

                        }
                        else
                        {
                            //turn left
                            if (direction == NORTH)
                            {
                                direction = WEST;
                            }
                            else
                            {
                                direction--;
                            }
                        }
                    }

                }
                //else do nothing at 1 - MOVE_PROB - TURN_PROB
            }
        }
    }
}
