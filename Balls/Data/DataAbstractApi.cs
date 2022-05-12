using System;
using System.Collections.ObjectModel;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public abstract void MoveBall(Ball ball);

        public abstract int getX(int i);
        public abstract int getY(int i);
        public abstract int getDiagonal(int i);
        public abstract int getHeight();
        public abstract int getWidth();
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }
    internal class DataApi : DataAbstractApi
    {
        public new readonly int Height = Board.height;
        public new readonly int Width = Board.width;
        public DataApi()
        {
            balls = new ObservableCollection<Ball>();
            Height = Board.height;
            Width = Board.width;
        }
        private ObservableCollection<Ball> balls { get; }
        public ObservableCollection<Ball> Balls => balls;
        public override int getWidth()
        {
            return Width;
        }
        public override int getHeight()
        {
            return Height;
        }

        public override int getX(int i)
        {
            return balls[i].X;
        }
        public override int getY(int i)
        {
            return balls[i].Y;
        }
        public override int getDiagonal(int i)
        {
            return 2 * balls[i].R;
        }

        public override void MoveBall(Ball ball)
        {
            if (ball.X + ball.R < Board.width - ball.R && ball.X + ball.R >= 0)
            {
                ball.X = ball.X + ball.R;
            }
            else
            {
                if (ball.X + ball.R >= Board.width)
                {
                    ball.X = Board.width - ball.R;
                }
                else
                {
                    ball.X = ball.R;
                }
                ball.R *= -1;
            }

            if (ball.Y + ball.R < Board.height - ball.R && ball.Y + ball.R > 0)
            {
                ball.Y = ball.Y + ball.R;
            }
            else
            {
                if ( ball.Y + ball.R >= Board.height)
                {
                    ball.Y = Board.height - ball.R;
                }
                else
                {
                    ball.Y = ball.R;
                }
                ball.R *= -1;
            }
            
        }
    }
}