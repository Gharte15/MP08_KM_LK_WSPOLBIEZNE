using System;
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
        double XSpeed { get; set; }
        double YSpeed { get; set; }
        int R { get;}
        double Weight { get; }
        int Identifier { get; }
        void Move(double time);
        void Stop();
        void CreateTask(int period);
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
            this.weight = weight;
            this.d = 2 * r;
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
                RaisePropertyChanged(nameof(X0));
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
                RaisePropertyChanged(nameof(Y0));
            }
        }

        public double XSpeed
        {
            get => xSpeed;
            set
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
            set
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
        public void NewVelociy(double x1, double y1)
        {
            lock (this)
            {
                XSpeed = xSpeed;
                YSpeed = ySpeed;
            }
        }
        public void Move(double time)
        {
            lock (this)
            {
                X0 += XSpeed * time;
                Y0 += YSpeed * time;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateTask(int period)
        {
            stop = false;
            task = Run(period);
        }

        private async Task Run(int period)
        {
            while (!stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                if (!stop)
                {
                    Move((period - stopwatch.ElapsedMilliseconds) / 16);
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