using System;
using Data;
namespace Logic
{
    public class Ball
    {
        private int x, y, r;
        public int X 
        { 
            get => x;
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
            }
        }

        public int R
        {
            get => r;
            set
            {
                r = value;
            }
        }

        public Ball(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            
        }

        public void MoveBall(int height, int width)
        {
            if (x + r < width && x + r > 0)
            {
                x = x + r;
            }
            else
            {
                if (x + r >= width)
                {
                    x = width;
                }
                else
                {
                    x = 0;
                }
                r *= -1;
            }

            if (y + r < height && y + r > 0)
            {
                y = y + r;
            }
            else
            {
                if (y + r >= height)
                {
                    y = height;
                }
                else
                {
                    y = 0;
                }
                r *= -1;
            }
        }

    }
}