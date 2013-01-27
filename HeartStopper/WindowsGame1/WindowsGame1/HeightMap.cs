using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    

    class HeightMap
    {
        const int MAX = 10;
        const int MIN = 3;

        const float INIT_RAND = 6;
        const float DELTA_RAND = 0.7f; // how much randomness changes per step.

        private int[,] map;
        int size;
        Random rand;
       

        public HeightMap(int size, int seed) {
            /* Size can only be 2^x + 1. seed of 0 is random seed. */
            this.size = size;

            Console.WriteLine("Random Level Seed: " + seed);

            if (seed > 0)
                rand = new Random(seed);
            else
                rand = new Random(); // Random seed.

            map = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = 0;
                }
            }

            // Set four corner values.
            map[0, 0] = rand.Next(MIN, MAX);
            map[0, size-1] = rand.Next(MIN, MAX);
            map[size-1, 0] = rand.Next(MIN, MAX);
            map[size-1, size-1] = rand.Next(MIN, MAX);

            diamondSquare(0, 16);
        }

        private void diamondSquare(int pass, float randomness)
        {
            int step = (size - 1) / (int) Math.Pow(2, pass);
            if (step < 2)
                return;

            Console.WriteLine(step);
            double sum;
            for (int i = 0; i < size - 1; i += step)
            {
                for (int j = 0; j < size - 1; j += step)
                {
                    sum = map[i, j] + map[i + step, j] + map[i, j + step] + map[i + step, j + step];
                    map[i + (step / 2), j + (step / 2)] = (int)Math.Round((float)sum / 4 + rand.Next((int)-randomness, (int)randomness));
                }
            }

            
            for (int i = 0; i < size; i += step)
            {
                for (int j = 0; j < size; j += step)
                {
                    // Diamond points could be outside of grid... be careful.

                    // Top point (always valid)
                    int count = 1;
                    sum = map[i, j];

                    int bdHeight = belowDiamondHeight(i, j, count, sum, step, randomness);
                    if (bdHeight >= 0)
                    {
                        map[i, j + (step / 2)] = bdHeight;
                    }

                    int rdHeight = rightDiamondHeight(i, j, count, sum, step, randomness);
                    if (rdHeight >= 0)
                    {
                        map[i + (step / 2), j] = rdHeight;
                    }

                    
                }
            }
            

            pass++;
            randomness *= DELTA_RAND;
            diamondSquare(pass, randomness);
        }

        private int belowDiamondHeight(int i, int j, int count, double sum, int step, float randomness)
        {
            if (j + (step / 2) >= size)
                return -1;

            int x, y;
            // Left point.
            x = i - (step / 2);
            y = j + (step / 2);
            if (x >= 0 && y < size)
            {
                sum += map[x, y];
                count++;
            }

            // Right point.
            x = i + (step / 2);
            y = j + (step / 2);
            if (x < size && y < size)
            {
                sum += map[x, y];
                count++;
            }

            // Bottom point.
            x = i;
            y = j + step;
            if (y < size)
            {
                sum += map[x, y];
                count++;
            }

            if (j + (step / 2) < size)
                return (int)Math.Round((float)sum / count + rand.Next((int)-randomness, (int)randomness));
            return -1;
        }

        private int rightDiamondHeight(int i, int j, int count, double sum, int step, float randomness)
        {
            if (i + (step / 2) >= size)
                return -1;
            
            int x, y;

            // Right point.
            x = i + step;
            y = j;
            if (x < size)
            {
                sum += map[x, y];
                count++;
            }

            // Up point.
            x = i + (step / 2);
            y = j - (step / 2);
            if (x < size && y >= 0)
            {
                sum += map[x, y];
                count++;
            }

            // Bottom point.
            x = i + (step / 2);
            y = j + (step /2 );
            if (x < size && y < size)
            {
                sum += map[x, y];
                count++;
            }

            if (i + (step / 2) < size)
                return (int)Math.Round((float)sum / count + rand.Next((int)-randomness, (int)randomness));
            return -1;
        }

        public int getHeight(int x, int y)
        {
            return map[y, x];
        }
    }
}
