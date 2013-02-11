using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
   public class Slope
    {
       public float s0, s45, s90, s135, s180, s225, s270, s315;
       private float _xForce, _yForce;
       public int range = 4;
       public Slope()
       {
       }
       private float getSlope(int h, int v, int baseHeight, int x, int y, HeightMap hmap)
       {
         

           float sum = 0;
           int h1 = baseHeight;
           int h2;
           for (int i = 0; i < range; i++)
           {
               h2 = hmap.getHeight(x + h * i, y + v * i);
               sum += h2 - h1;
               h1 = h2;
           }
           return sum / range;
       }
       public void doSlope(int baseHeight, int x, int y, HeightMap hmap)
       {
           s0 = getSlope(1, 0, baseHeight, x, y, hmap);
           s45 = getSlope(1, 1, baseHeight, x, y, hmap);
           s90 = getSlope(0, 1, baseHeight, x, y, hmap);
           s135 = getSlope(-1, 1, baseHeight, x, y, hmap);
           s180 = getSlope(-1, 0, baseHeight, x, y, hmap);
           s225 = getSlope(-1, -1, baseHeight, x, y, hmap);
           s270 = getSlope(0, -1, baseHeight, x, y, hmap);
           s315 = getSlope(1, -1, baseHeight, x, y, hmap);

           _xForce = 0;
           _yForce = 0;

           _xForce -= (s0 + s180 * -1);
           _yForce -= (s90 + s270 * -1);

           float angleSlope1 = s135 + s315 * -1;
           float angleSlope2 = s45 + s225 * -1;

           _xForce -= (angleSlope1 / 2 + angleSlope2 / 2);
           _yForce -= (angleSlope1 / 2 + angleSlope2 / 2);
       }
       public float xForce
       {
           get{return _xForce;}
       }
       public float yForce
       {
           get { return _yForce; }
       }
    }
}
