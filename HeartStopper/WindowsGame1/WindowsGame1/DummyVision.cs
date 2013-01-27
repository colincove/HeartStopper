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
    public class DummyVision : Microsoft.Xna.Framework.GameComponent
    {
        const int DIR_NONE = 0;
        const int DIR_LEFT = -1;
        const int DIR_RIGHT = 1;
        const int DIR_UP = -1;
        const int DIR_DOWN = 1;

        const int MAX_HIGHLIGHTS = 1024;
        const int VISION_RANGE = 100; // Starting 'vision range' resource. Delta elevation of surrounding terrain tied to cost of seeing.

        const int VCOST_EQUAL = VISION_RANGE / 30;
        const int VCOST_DOWNHILL = VISION_RANGE / 80;
        const int VCOST_UPHILL = VISION_RANGE / 6;

        Game1 game;
        int x;
        int y;
        int dirLR; // Left/right direction
        int dirUD; // up/down direction
        KeyboardState oldState;

        VisionHighlight[] highlights;
        private int highlightIndex;

        public DummyVision(Game1 game, int x, int y)
            : base(game)
        {
            // TODO: Construct any child components here
            this.game = game;
            this.x = x;
            this.y = y;
            this.dirLR = DIR_NONE;
            this.dirUD = DIR_NONE;
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
                highlights[i] = new VisionHighlight(this.game, this.x, this.y, (int) (200 - (((float) i / (float) MAX_HIGHLIGHTS) * 600)));
            }
            base.Initialize();

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {   
            KeyboardState newState = Keyboard.GetState();

            if (oldState != null)
            {
                // TODO: Add your update code here
                if (newState.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A))
                {
                    x--;
                    this.dirLR = DIR_LEFT;
                    this.dirUD = DIR_NONE;
                }
                if (newState.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D))
                {
                    x++;
                    this.dirLR = DIR_RIGHT;
                    this.dirUD = DIR_NONE;
                }
                if (newState.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W))
                {
                    y--;
                    this.dirUD = DIR_UP;
                    this.dirLR = DIR_NONE;
                }
                if (newState.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S))
                {
                    y++;
                    this.dirUD = DIR_DOWN;
                    this.dirLR = DIR_NONE;
                }
            }

            updateHighlights();

            oldState = newState;
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

            castVisionCone(x, y, dirLR, dirUD, VISION_RANGE, 1);
        }

        private void castVisionCone(int x, int y, int dx, int dy, int total, int quota)
        {
            if (x >= Game1.MAP_SIZE || y >= Game1.MAP_SIZE || x < 0 || y < 0)
                return;

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
                VisionHighlight hl = highlights[highlightIndex];
                hl.drawIt = true;
                hl.setPosition(new Vector2(x + dx, y + dy));
                highlightIndex++;

                castVisionCone(x + dx, y + dy, dx, dy, total, quota);
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
