﻿using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public abstract int GetCount();
        public abstract void UpdateBalls();
        public abstract bool DetectCollision(Ball ball1, Ball ball2);
        public abstract void MoveBall(Ball ball);
        public abstract IList CreateBalls(int count);
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

        public override IList CreateBalls(int number)
        {
            Random random = new Random();
            if (number > 0)
            {
                for (int i = 0; i < number; i++)
                {
                    int r = 20;
                    int x = random.Next(r, Board.width - r);
                    int y = random.Next(r, Board.height - r);
                    Ball ball = new Ball(x, y, r);
                    balls.Add(ball);
                }
            }
            return balls;
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
        public override int GetCount()
        {
            int counter = 0;
            foreach(Ball ball in balls)
            {
                counter += 1;
            }
            return counter;
        }

        public override void UpdateBalls()
        {
            foreach (Ball ball in balls)
            {
                MoveBall(ball);
            }
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (i != j)
                    {
                        if (DetectCollision(balls[i], balls[j]))
                        {
                            balls[i].R *= -1;
                            balls[j].R *= -1;
                        }
                    }
                }
            }
        }

        public override bool DetectCollision(Ball ball1, Ball ball2)
        {
            bool flag = false;

            if ((ball1.X - ball2.X <= ball2.R) && (ball1.Y - ball2.Y == ball2.R))
            {
                flag = true;
            }

            return flag;
        }

    }
}