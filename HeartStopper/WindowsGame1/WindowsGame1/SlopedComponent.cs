using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using HeartStopper;

namespace WindowsGame1
{
    public class SlopedComponent : DepthComponent
    {
        protected Slope slope;
        protected Vector2 cachePos = new Vector2(0, 0);
        protected bool applyForce = false;
        protected float damp = 1.0f;
        public SlopedComponent(Game1 game)
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
            
            //do slope forces
            if (applyForce)
            {
                velX += slope.xForce / damp;
                velY += slope.yForce / damp;
            }
        }
        private void doSlope()
        {
            int baseHeight = ((Game1)(Game)).map.hmap.getHeight((int)(x / Tile.TILE_SIZE_2), (int)(y / Tile.TILE_SIZE_2));

            int gridX = (int)(x / Tile.TILE_SIZE_2);
            int gridY = (int)(y / Tile.TILE_SIZE_2);
            slope.doSlope(baseHeight, gridX, gridY, ((Game1)(Game)).map.hmap);
        }
    }
}
