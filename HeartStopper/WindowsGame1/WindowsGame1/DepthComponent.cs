using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartStopper;

namespace WindowsGame1
{
    public class DepthComponent : DrawableSprite
    {
        protected Slope slope;
        protected Vector2 cachePos = new Vector2(0, 0);
        protected float velX = 0.0f;
        protected float velY = 0.0f;
        public DepthComponent(Game1 game)
            : base(game)
        {
            slope = new Slope();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if ((int)(x / Tile.TILE_SIZE_2) != cachePos.X || (int)(y / Tile.TILE_SIZE_2) != cachePos.Y)
            {
                cachePos.X = (int)(x / Tile.TILE_SIZE_2);
                cachePos.Y = (int)(y / Tile.TILE_SIZE_2);
                doSlope();
            }
            float damp = 10f;
            //do slope forces
            velX -= (slope.s0 + slope.s180 * -1) / damp;
            velY -= (slope.s90 + slope.s270 * -1) / damp;

            float angleSlope1 = slope.s135 + slope.s315 * -1;
            float angleSlope2 = slope.s45 + slope.s225 * -1;

            velX -= (angleSlope1 / 2 + angleSlope2 / 2) / damp;
            velY -= (angleSlope1 / 2 + angleSlope2 / 2) / damp;

            x += velX/3;
            y += velY/3;

        }
        private void doSlope()
        {
            int baseHeight = ((Game1)(Game)).map.hmap.getHeight((int)(x / Tile.TILE_SIZE_2), (int)(y / Tile.TILE_SIZE_2));

            int gridX = (int)(x / Tile.TILE_SIZE_2);
            int gridY = (int)(y / Tile.TILE_SIZE_2);
            slope.s0 = getSlope(1, 0, baseHeight, gridX, gridY);
            slope.s45 = getSlope(1, 1, baseHeight, gridX, gridY);
            slope.s90 = getSlope(0, 1, baseHeight, gridX, gridY);
            slope.s135 = getSlope(-1, 1, baseHeight, gridX, gridY);
            slope.s180 = getSlope(-1, 0, baseHeight, gridX, gridY);
            slope.s225 = getSlope(-1, -1, baseHeight, gridX, gridY);
            slope.s270 = getSlope(0, -1, baseHeight, gridX, gridY);
            slope.s315 = getSlope(1, -1, baseHeight, gridX, gridY);
            Console.WriteLine(slope.s0);
            //Console.WriteLine(slope.s0);
        }
        private float getSlope(int h, int v, int baseHeight, int x, int y)
        {
            int range = 4;

            float sum = 0;
            int h1 = baseHeight;
            int h2;
            for (int i = 0; i < range; i++)
            {
                h2 = ((Game1)(Game)).map.hmap.getHeight(x + h * i, y + v * i);
                sum += h2 - h1;
                h1 = h2;
            }
            return sum / range;
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
