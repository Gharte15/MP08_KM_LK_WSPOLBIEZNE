using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract IList CreateBalls(int count);
        public abstract void Start();
        public abstract void Stop();
        //public abstract void WallCollision(IBall ball);
        //public abstract void ChangeDirection(IBall ball);
        //public abstract void BallPositionChanged(object sender, PropertyChangedEventArgs args);
        public static LogicAbstractApi CreateApi()
        {
            return new LogicApi();
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
        private ObservableCollection<IBall> balls { get; }
        private int width;
        private int height;
        private readonly DataAbstractApi dataLayer;

        public LogicApi()
        {
            balls = new ObservableCollection<IBall>();
            dataLayer = DataAbstractApi.CreateApi();
            this.width = dataLayer.Width;
            this.height = dataLayer.Height;

        }

        public override int Width { get; }
        public override int Height { get; }
        public ObservableCollection<IBall> Balls => balls;
        public override void Start()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].CreateTask(30);
            }
        }

        public override void Stop()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Stop();

            }
        }


        internal void WallCollision(IBall ball)
        {

            int diameter = ball.R * 2;

            int rightBorder = this.width - diameter;

            int bottomBorder = this.height - diameter;

            
            if (ball.X0 <= ball.R)
            {
                if (ball.XSpeed <= 0)
                {
                    ball.XSpeed = -ball.XSpeed;
                }
            }

            else if (ball.X0 >= rightBorder)
            {
                if (ball.XSpeed > 0)
                {
                    ball.XSpeed = -ball.XSpeed;
                }
            }
            if (ball.Y0 <= ball.R)
            {
                if (ball.YSpeed <= 0)
                {
                    ball.YSpeed = -ball.YSpeed;
                }
            }

            else if (ball.Y0 >= bottomBorder)
            {
                if (ball.YSpeed > 0)
                {
                    ball.YSpeed = -ball.YSpeed;
                }
            }
            
        }

        internal void ChangeDirection(IBall ball)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                IBall secondBall = balls[i];
                if (ball.Identifier == secondBall.Identifier)
                {
                    continue;
                }

                if (DetectCollision(ball, secondBall))
                {
                    lock (ball)
                    {
                        double m1 = ball.Weight;
                        double v1x = ball.XSpeed;
                        double v1y = ball.YSpeed;
                        double u1x;
                        double u1y;
                        
                        double v2x = secondBall.XSpeed;
                        double v2y = secondBall.YSpeed;
                        double m2 = secondBall.Weight;
                        u1x = (m1 - m2) * v1x / (m1 + m2) + (2 * m2) * v2x / (m1 + m2);
                        u1y = (m1 - m2) * v1y / (m1 + m2) + (2 * m2) * v2y / (m1 + m2);
                        double u2x = 2 * m1 * v1x / (m1 + m2) + (m2 - m1) * v2x / (m1 + m2);
                        double u2y = 2 * m1 * v1y / (m1 + m2) + (m2 - m1) * v2y / (m1 + m2);
                        secondBall.XSpeed = u2x;
                        secondBall.YSpeed = u2y;
                        
                        ball.XSpeed = u1x;
                        ball.YSpeed = u1y;
                    }
                }

            }


        }
        internal bool DetectCollision(IBall a, IBall b)
        {
            bool flag = false;
            if (a == null || b == null)
            {
                return false;
            }
            if (Distance(a, b) <= (2 * a.R))
            {
                flag = true;
            }

            return flag;
        }

        internal double Distance(IBall a, IBall b)
        {
            double x1, y1, x2, y2;
            lock (this)
            {
                x1 = a.X0 + a.R + a.XSpeed;
                y1 = a.Y0 + a.R + a.YSpeed;
                x2 = b.X0 + b.R + b.YSpeed;
                y2 = b.Y0 + b.R + b.YSpeed;

            }
            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        public override IList CreateBalls(int number)
        {
            for (int i = 0; i < number; i++)
            {
                bool contain = true;
                bool count;

                while (contain)
                {
                    balls.Add(dataLayer.CreateBall(i + 1));
                    count = false;
                    for (int j = 0; j < i; j++)
                    {

                        if (balls[i].X0 <= balls[j].X0 + 2*balls[j].R && balls[i].X0 + 2*balls[i].R >= balls[j].X0)
                        {
                            if (balls[i].Y0 <= balls[j].Y0 + 2*balls[j].R && balls[i].Y0 + 2*balls[i].R >= balls[j].Y0)
                            {

                                count = true;
                                balls.Remove(balls[i]);
                                break;
                            }
                        }
                    }
                    if (!count)
                    {
                        contain = false;
                    }
                }
                balls[i].PropertyChanged += BallPositionChanged;
            }
            return balls;
        }

        internal void BallPositionChanged(object sender, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)sender;
            WallCollision(ball);
            ChangeDirection(ball);

        }


    }
}