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
using WindowsGame1;

namespace HeartStopper
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Map : Microsoft.Xna.Framework.GameComponent
    {

        public Tile[,] grid;
        private int width;
        private int height;
        private Game game;

        public Map(Game game, int width, int height)
            : base(game)
            
        {
            // TODO: Construct any child components here
            this.game = game;
            this.width = width;
            this.height = height;
            this.game.Components.Add(this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();

            // Init the map in a dumb way for now.
            grid = new Tile[width, height];

            int[,] elevationMap = new int[width, height];

            // ***** This hard-coded elevation map is 21x11 ******
            elevationMap = new int[,]
            //  0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16,17,18,19,20
            { { 1, 2, 3, 3, 4, 4, 4, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 0
              { 1, 2, 3, 4, 4, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 1
              { 1, 2, 3, 4, 5, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 2
              { 1, 2, 3, 4, 5, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 3
              { 1, 2, 3, 4, 5, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 4
              { 1, 2, 3, 4, 5, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 5
              { 1, 2, 3, 4, 5, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 6
              { 1, 2, 3, 4, 4, 5, 5, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 7
              { 1, 2, 3, 3, 4, 4, 4, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 8
              { 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 9
              { 1, 1, 2, 2, 3, 3, 4, 4, 5, 6, 5, 4, 3, 4, 5, 6, 7, 8, 9, 8, 7}, // 10
            };
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Tile(game, new Vector2(i,j), elevationMap[j, i]);
                }
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
