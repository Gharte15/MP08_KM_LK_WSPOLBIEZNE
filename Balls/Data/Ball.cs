using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        double X0 { get; }
        double Y0 { get; }
        double XSpeed { get; }
        double YSpeed { get; }
        int R { get;}
        int D { get;}
        double Weight { get; }
        int Identifier { get; }
        void SaveRequest(ConcurrentQueue<IBall> queue);
        void NewVelocity(double xSpeed, double ySpeed);
        void Move(double time, ConcurrentQueue<IBall> queue);
        void Stop();
        void CreateTask(int period, ConcurrentQueue<IBall> queue);
    }

    internal class Ball : IBall
    {
        private double x0;
        private double y0;
        private double xSpeed;
        private double ySpeed;
        private readonly int r;
        private readonly int d;
        private readonly double weight;
        private readonly int identifier;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private Task task;
        private bool stop = false;

        public Ball(int id, double x0, double y0, double xSpeed, double ySpeed, int r, double weight)
        {
            identifier = id;
            this.x0 = x0;
            this.y0 = y0;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.r = r;
            this.d = 2 * r;
            this.weight = weight;
        }

        public int Identifier { get => identifier; }
        public double X0
        {
            get => x0;
            private set
            {
                if (value.Equals(x0))
                {
                    return;
                }

                x0 = value;
               // RaisePropertyChanged(nameof(X0));
            }
        }
        public double Y0
        {
            get => y0;
            private set
            {
                if (value.Equals(y0))
                {
                    return;
                }

                y0 = value;
               // RaisePropertyChanged(nameof(Y0));
            }
        }

        public double XSpeed
        {
            get => xSpeed;
            private set
            {
                if (value.Equals(xSpeed))
                {
                    return;
                }

                xSpeed = value;
            }
        }
        public double YSpeed
        {
            get => ySpeed;
            private set
            {
                if (value.Equals(ySpeed))
                {
                    return;
                }

                ySpeed = value;
            }
        }

        public int R { get => r; }
        public int D { get => d; }
        public double Weight { get => weight; }
        public void NewVelocity(double xSpeed, double ySpeed)
        {
            lock (this)
            {
                XSpeed = xSpeed;
                YSpeed = ySpeed;
            }         
        }
        public void Move(double time, ConcurrentQueue<IBall> queue)
        {
            lock (this)
            {
                X0 += XSpeed * time;
                RaisePropertyChanged(nameof(X0));
                Y0 += YSpeed * time;
                RaisePropertyChanged(nameof(Y0));
                SaveRequest(queue);
            }

        }
        public void SaveRequest(ConcurrentQueue<IBall> queue)
        {
            queue.Enqueue(new Ball(this.Identifier, this.X0, this.Y0, this.XSpeed, this.YSpeed, this.R, this.Weight));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateTask(int period, ConcurrentQueue<IBall> queue)
        {
            stop = false;
            task = Run(period, queue);
        }

        private async Task Run(int period, ConcurrentQueue<IBall> queue)
        {
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                if (!stop)
                {
                    Move(((period - stopwatch.ElapsedMilliseconds) / 16), queue);
                }
                stopwatch.Stop();

                await Task.Delay((int)(period - stopwatch.ElapsedMilliseconds));
            }
        }
        public void Stop()
        {
            stop = true;
        }
    }
}