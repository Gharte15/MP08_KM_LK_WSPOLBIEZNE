using System;
using System.Collections;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract int getXcoordinate(int i);
        public abstract int getYcoordinate(int i);
        public abstract int getSize(int i);
        public abstract IList CreateBallsList(int count);
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

        public LogicApi()
        {
            data = DataAbstractApi.CreateApi();
            
        }
        public override IList CreateBallsList(int number) => data.CreateBalls(number);

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

        public override int GetCount => data.GetCount;

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
                        if (DetectCollision((Data.Ball)balls[i], (Data.Ball)balls[j]))
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