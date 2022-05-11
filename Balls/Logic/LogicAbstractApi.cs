using System;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract event EventHandler Update;
        public abstract int Width { get; }
        public abstract int Height { get; }
        protected int width, height;
        public abstract List<Ball> balls { get; }
        public abstract void CreateBallsList(int count);
        public abstract void UpdateBalls();
        public abstract void Start();
        public abstract void Stop();
        public abstract void SetInterval(int ms);
        public abstract int GetDiagonal(int i);
        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetCount { get; }

        public static LogicAbstractApi CreateApi(int height, int width, TimerApi timer = default(TimerApi))
        {
            height = Board.height;
            width = Board.width;
            return new LogicApi(height, width, timer ?? TimerApi.CreateBallTimer());
        }

    }
    internal class LogicApi : LogicAbstractApi
    {
        private readonly TimerApi timer;
        private DataAbstractApi data;

        public override int Width 
        { 
            get { return width; } 
        }
        public override int Height 
        { 
            get { return height; } 
        }

        public override List<Ball> balls 
        { 
            get; 
        }

        public LogicApi(int height, int width, TimerApi WPFTimer)
        {
            data = DataAbstractApi.CreateApi();
            width = Board.width;
            height = Board.height;
            timer = WPFTimer;
            balls = new List<Ball>();
            SetInterval(25);
            timer.Tick += (sender, args) => UpdateBalls();
        }
        public override void CreateBallsList(int number)
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
        }

        public override event EventHandler Update { add => timer.Tick += value; remove => timer.Tick -= value; }

        public override int GetDiagonal(int i)
        {
            return 2 * balls[i].R;
        }
        public override int GetX(int i)
        {
            return balls[i].X;
        }

        public override int GetY(int i)
        {
            return balls[i].Y;
        }

        public override int GetCount { get => balls.Count; }

        public override void UpdateBalls()
        {
            foreach (Ball ball in balls)
            {
                ball.MoveBall(Board.height, Board.width);

            }
        }

        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }

        public override void SetInterval(int ms)
        {
            timer.Interval = TimeSpan.FromMilliseconds(ms);
        }
    }
}