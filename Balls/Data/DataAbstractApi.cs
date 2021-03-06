using System;
using System.Collections;
using System.Collections.Concurrent;
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
        public abstract IBall CreateBall(int count);
        public abstract void StopLoggingTask();
        public abstract Task CreateLoggingTask(ConcurrentQueue<IBall> logQueue);
        public static DataAbstractApi CreateApi()
        {
            return new DataApi();
        }
    }
    internal class DataApi : DataAbstractApi
    {
        private readonly Stopwatch stopWatch;
        private readonly Random random = new Random();
        private bool stop;
        private object locker = new object();

        public override int Width { get; }
        public override int Height { get; }
        public DataApi()
        {
            Height = Board.height;
            Width = Board.width;
            stopWatch = new Stopwatch();
        }
        public override IBall CreateBall(int count)
        {
            int radius = 10;
            double weight = radius;

            double x = random.Next(radius + 10, Width - radius - 10);
            double y = random.Next(radius + 10, Height - radius - 10);
            double xSpeed = 0;
            double ySpeed = 0;
            while (xSpeed == 0)
            {
                xSpeed = random.Next(-5, 5) + random.NextDouble();
            }
            while (ySpeed == 0)
            {
                ySpeed = random.Next(-5, 5) + random.NextDouble();
            }
            Ball ball = new Ball(count, x, y, xSpeed, ySpeed, radius, weight);

          return ball;
        }

        public override void StopLoggingTask()
        {
            stop = true;
        }

        public override Task CreateLoggingTask(ConcurrentQueue<IBall> logQueue)
        {
            stop = false;
            return CallLogger(logQueue);
        }

        internal async Task CallLogger(ConcurrentQueue<IBall> logQueue)
        {
            while (!stop)
            {
                stopWatch.Reset();
                stopWatch.Start();
                logQueue.TryDequeue(out IBall logObject);
                if (logObject != null)
                {
                    string diagnostics = JsonSerializer.Serialize(logObject);
                    string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                    string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", date, diagnostics) + "}";

                    lock (locker)
                    {
                        File.AppendAllText("BallsLogQueue.json", log);
                    }
                }
                else 
                { 
                    return; 
                }
                stopWatch.Stop();
                await Task.Delay((int)(stopWatch.ElapsedMilliseconds));
            }
        }
    }
}