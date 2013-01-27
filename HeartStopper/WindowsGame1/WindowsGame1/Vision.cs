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

namespace WindowsGame1
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Vision : Microsoft.Xna.Framework.GameComponent
    {

        const int DIR_UP = 0;
        const int DIR_UPRIGHT = 1;
        const int DIR_RIGHT = 2;
        const int DIR_DOWNRIGHT = 3;
        const int DIR_DOWN1 = 4;
        const int DIR_DOWN2 = -4;
        const int DIR_DOWNLEFT = -3;
        const int DIR_LEFT = -2;
        const int DIR_UPLEFT = -1;
        
        const int MAX_HIGHLIGHTS = 4096;
        const int VISION_RANGE = 1024; // Starting 'vision range' resource. Delta elevation of surrounding terrain tied to cost of seeing.

        const int VCOST_EQUAL = VISION_RANGE / 80;
        const int VCOST_DOWNHILL = VISION_RANGE / 160;
        const int VCOST_UPHILL = VISION_RANGE / 8;

        Game1 game;
        DrawableSprite parent;
        int roundedDir;

        VisionHighlight[] highlights;
        private int highlightIndex;

        Vector2 tile;

        public Vision(Game1 game, DrawableSprite parent)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.parent = parent;
            //this.dirLR = DIR_NONE;
            //this.dirUD = DIR_NONE;
            this.game.Components.Add(this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            highlights = new VisionHighlight[MAX_HIGHLIGHTS];
            for (int i = 0; i < MAX_HIGHLIGHTS; i++)
            {
                
                highlights[i] = new VisionHighlight(this.game, 0, 0, (int) (200 - (((float) i / (float) MAX_HIGHLIGHTS) * 600)));
            }

            tile = new Vector2(0, 0);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            roundedDir = (int) Math.Round((parent.angle * 4.0f / Math.PI));
            updateHighlights();
            base.Update(gameTime);
        }

        private void updateHighlights()
        {
            // Turn all highlights off.
            highlightIndex = 0;
            while (highlights[highlightIndex].drawIt)
            {
                highlights[highlightIndex].drawIt = false;
                highlightIndex++;
            }
            highlightIndex = 0;

            int dirLR = 1;
            int dirUD = 1;
            switch (roundedDir)
            {
                case DIR_UP:
                    dirLR = 0;
                    dirUD = -1;
                    break;
                case DIR_UPRIGHT:
                    dirLR = 1;
                    dirUD = -1;
                    break;
                case DIR_RIGHT:
                    dirLR = 1;
                    dirUD = 0;
                    break;
                case DIR_DOWNRIGHT:
                    dirLR = 1;
                    dirUD = 1;
                    break;
                case DIR_DOWN1:
                case DIR_DOWN2:
                    dirLR = 0;
                    dirUD = 1;
                    break;
                case DIR_DOWNLEFT:
                    dirLR = -1;
                    dirUD = 1;
                    break;
                case DIR_LEFT:
                    dirLR = -1;
                    dirUD = 0;
                    break;
                case DIR_UPLEFT:
                    dirLR = -1;
                    dirUD = -1;
                    break;
                default:
                    dirLR = 0;
                    dirUD = 0;
                    break;
            }

            tile = game.map.getTile(parent.x + game.cam.X + Game1.VIEWPORT_WIDTH/2, parent.y + game.cam.Y + Game1.VIEWPORT_HEIGHT/2);
            //Console.WriteLine(tile.X + " " + tile.Y);
            castVisionCone((int) tile.X, (int) tile.Y, dirLR, dirUD, VISION_RANGE, 1);
        }

        private void castVisionCone(int x, int y, int dx, int dy, int total, int quota)
        {
            if (x >= Game1.MAP_SIZE || y >= Game1.MAP_SIZE || x < 0 || y < 0)
            {
                Console.WriteLine(x + " " + y);
                return;
            }
            if (highlightIndex >= MAX_HIGHLIGHTS)
                return;

            if (dx == 0)
            {
                castVisionLine(x, y, 1, 0, total, quota);
                castVisionLine(x, y, -1, 0, total, quota);
            }
            else if (dy == 0)
            {
                castVisionLine(x, y, 0, 1, total, quota);
                castVisionLine(x, y, 0, -1, total, quota);
            }
            else
            { // Moving in a diagonal.
                castVisionLine(x, y, -dx, 0, total, quota);
                castVisionLine(x, y, 0, -dy, total, quota);
            }

            if (x + dx >= Game1.MAP_SIZE || y + dy >= Game1.MAP_SIZE || x + dx < 0 || y + dy < 0)
                return;

            int currEle = game.map.grid[x, y];
            int nextEle = game.map.grid[x + dx, y + dy];

            int deltaElevation = currEle - nextEle;

            if (deltaElevation == 0)
                total -= VCOST_EQUAL;
            else if (deltaElevation < 0)
                total -= VCOST_DOWNHILL;
            else if (deltaElevation > 0)
                total -= VCOST_UPHILL;
            else
                return; // Shouldn't happen...

            quota++;

            if (total >= 0)
            {
                if (highlightIndex < MAX_HIGHLIGHTS)
                {
                    VisionHighlight hl = highlights[highlightIndex];
                    hl.drawIt = true;
                    hl.setPosition(new Vector2(x + dx, y + dy));
                    highlightIndex++;

                    castVisionCone(x + dx, y + dy, dx, dy, total, quota);
                }
            }
        }

        private void castVisionLine(int x, int y, int dx, int dy, int total, int quota)
        {
            
            if (highlightIndex >= MAX_HIGHLIGHTS || quota <= 0)
                return;

            if (x >= Game1.MAP_SIZE || y >= Game1.MAP_SIZE || x < 0 || y < 0)
                return;

            if (x + dx >= Game1.MAP_SIZE || y + dy >= Game1.MAP_SIZE || x + dx < 0 || y + dy < 0)
                return;

            int currEle = game.map.grid[x, y];
            int nextEle = game.map.grid[x + dx, y + dy];

            int deltaElevation = currEle - nextEle;

            if (deltaElevation == 0)
                total -= VCOST_EQUAL;
            else if (deltaElevation < 0)
                total -= VCOST_DOWNHILL;
            else if (deltaElevation > 0)
                total -= VCOST_UPHILL;
            else
                return; // Shouldn't happen...

            if (total >= 0)
            {
                VisionHighlight hl = highlights[highlightIndex];
                hl.drawIt = true;
                hl.setPosition(new Vector2(x + dx, y + dy));
                highlightIndex++;
                quota--;
                castVisionLine(x + dx, y + dy, dx, dy, total, quota);
            }
        }
    }
}
