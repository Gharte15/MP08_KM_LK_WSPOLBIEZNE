using System;
using Data;

namespace Logic
{
    public class Ball
    {
        private int x, y, r, v;
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public int V { get; set; }

        public Ball(int x, int y, int r, int v)
        {
            X = x;
            Y = y;
            R = r;
            V = v;
        }

        public void MoveBall(int height, int width)
        {
            if(x + r < width && x + r > 0)
            {
                x = x + r;
            }
            else
            {
                if(x + r == width)
                {
                    x = width;
                }
                else
                {
                    x = 0;
                }
            }
            
            if(y + r < height && y + r > 0)
            {
                y = y + r;
            }
            else
            {
                if(y + r == height)
                {
                    y = height;
                }
                else
                {
                    y = 0;
                }
            }
        }

    }
}
