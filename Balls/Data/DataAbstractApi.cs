﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int GetCount { get; }
        public abstract IList CreateBalls(int number);

        public abstract IBall GetBall(int index);
        public abstract void AppendToFile(string filename, BallCollisionLog ballCollisionLog);
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }
    internal class DataApi : DataAbstractApi
    {
        private ObservableCollection<IBall> balls { get; }
        private readonly Mutex mutex = new Mutex();

        private readonly Random random = new Random();

        public override int Width { get; }
        public override int Height { get; }
        public ObservableCollection<IBall> Balls => balls;
        public DataApi()
        {
            balls = new ObservableCollection<IBall>();
            Height = Board.height;
            Width = Board.width;
        }
        public override IList CreateBalls(int number)
        {
            Random random = new Random();
            if (number > 0)
            {
                int ballsCount = balls.Count;
                for (int i = 0; i < number; i++)
                {
                    mutex.WaitOne();
                    int Xrandomizer = random.Next(1, 2);
                    int Yrandomizer = random.Next(1, 2);
                    int r = 10;
                    int weight = 30;
                    int x0 = random.Next(r, Width - r);
                    int y0 = random.Next(r, Height - r);

                    int x1 = random.Next(-5, 5);
                    int y1 = random.Next(-5, 5);
                    
                    Ball ball = new Ball(i + ballsCount, x0, y0, x1, y1, r, weight);
                    balls.Add(ball);
                    mutex.ReleaseMutex();
                }
            }
                return balls;
        }
        public override int GetCount { get => balls.Count; }

        public override IBall GetBall(int index)
        {
            return balls[index];
        }

        public override void AppendToFile(string filename, BallCollisionLog ballCollisionLog)
        {
            string collisionInfo = JsonSerializer.Serialize(ballCollisionLog);
            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            string collisionLog = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"CollisionBetween\":{1}\n", date, collisionInfo) + "}";
            File.AppendAllText(filename, collisionLog);
        }

    }
}