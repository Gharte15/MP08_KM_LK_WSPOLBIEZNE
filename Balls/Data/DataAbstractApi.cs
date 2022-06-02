using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract int GetCount { get; }
        public abstract IList CreateBalls(int number);
        public abstract IList GetBalls();

        public abstract IBall GetBall(int index);
        public abstract void StopLoggingTask();

        public abstract Task CreateLoggingTask(int interval, IList Balls);
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
        private readonly Stopwatch stopWatch;

        private readonly Random random = new Random();
        private bool stop;

        public override int Width { get; }
        public override int Height { get; }
        public ObservableCollection<IBall> Balls => balls;
        public DataApi()
        {
            balls = new ObservableCollection<IBall>();
            Height = Board.height;
            Width = Board.width;
            stopWatch = new Stopwatch();
        }
        public override IList CreateBalls(int number)
        {
            Random random = new Random();
            if (number > 0)
            {
                int ballsCount = balls.Count;
                for (int i = 0; i < number; i++)
                {
                    int r = 10;
                    int weight = 30;
                    int x0 = random.Next(2*r, Width - 2*r);
                    int y0 = random.Next(2*r, Height - 2*r);

                    int x1 = random.Next(-5, 5);
                    int y1 = random.Next(-5, 5);
                    
                    Ball ball = new Ball(i + ballsCount, x0, y0, x1, y1, r, weight, 0);
                    balls.Add(ball);
                   
                }
            }
                return balls;
        }
        public override int GetCount { get => balls.Count; }

        public override IBall GetBall(int index)
        {
            return balls[index];
        }
        public override IList GetBalls()
        {
            return balls;
        }

        public override void StopLoggingTask()
        {
            stop = true;
        }

        public override Task CreateLoggingTask(int interval, IList Balls)
        {
            stop = false;
            return CallLogger(interval, Balls);
        }

        internal async Task CallLogger(int interval, IList Balls)
        {
            while (!stop)
            {
                stopWatch.Reset();
                stopWatch.Start();
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonBalls = JsonSerializer.Serialize(balls, options);
                string now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");

                string newJsonObject = "{" + String.Format("\n\t\"datetime\": \"{0}\",\n\t\"balls\":{1}\n", now, jsonBalls) + "}";
                mutex.WaitOne();
                File.AppendAllText("BallsListLog.json", newJsonObject);
                mutex.ReleaseMutex();
                stopWatch.Stop();
                await Task.Delay((int)(interval - stopWatch.ElapsedMilliseconds));
            }
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