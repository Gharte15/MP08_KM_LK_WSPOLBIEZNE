using System;
using System.Collections;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract event EventHandler Update;
        public abstract IList balls { get; }
        public abstract int getXcoordinate(int i);
        public abstract int getYcoordinate(int i);
        public abstract int getSize(int i);
        public abstract void CreateBallsList(int count);
        public abstract void UpdateBalls();
        public abstract bool DetectCollision(Ball ball1, Ball ball2);
        public abstract void Start();
        public abstract void Stop();
        public abstract int GetCount { get; }

        public static LogicAbstractApi CreateApi()
        {
            return new LogicApi();
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
        private DataAbstractApi data;

        public override IList balls 
        { 
            get; 
        }

        public LogicApi()
        {
            data = DataAbstractApi.CreateApi();
            balls = new List<Ball>();
        }
        public override void CreateBallsList(int number)
        {
            Random random = new Random();
            if (number > 0)
            {
                for (int i = 0; i < number; i++)
                {
                    int r = 20;
                    int x = random.Next(r, data.Width - r);
                    int y = random.Next(r, data.Height - r);
                    Ball ball = new Ball(x, y, r);
                    balls.Add(ball);
                }
            }
        }

        public override int getSize(int i)
        {
            return data.getDiagonal(i);
        }
        public override int getXcoordinate(int i)
        {
            return data.getX(i);
        }

        public override int getYcoordinate(int i)
        {
            return data.getY(i);
        }

        public override int GetCount { get => balls.Count; }

        public override void UpdateBalls()
        {
            foreach (Ball ball in balls)
            {
                data.MoveBall(ball);
            }
            for(int i = 0; i < balls.Count; i++)
            {
                for(int j = 0; j < balls.Count; j++)
                {
                    if(i != j)
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

            if((ball1.X - ball2.X <= ball2.R) && (ball1.Y - ball2.Y == ball2.R))
            {
                flag = true;
            }

                return flag;
        }

        public override void Start()
        {
            
        }

        public override void Stop()
        {
            
        }
    }
}