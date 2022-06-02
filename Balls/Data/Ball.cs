﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        double X0 { get; set; }
        double Y0 { get; set; }
        double X1 { get; set; }
        double Y1 { get; set; }
        int R { get;}
        double Weight { get; }
        double BetweenBallsCollisions { get; set; }
        double BetweenWallCollisions { get; set; }

        int Identifier { get; }
        void Move(double time);
        void Stop();
        void CreateTask(int period);
    }

    internal class Ball : IBall
    {
        private double x0;
        private double y0;
        private double x1;
        private double y1;
        private double betweenBallsCollisions;
        private double betweenWallCollisions;
        private readonly int r;
        private readonly int d;
        private readonly double weight;
        private readonly int identifier;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private Task task;
        private bool stop = false;

        public Ball(int id, double x0, double y0, double x1, double y1, int r, double weight, double c)
        {
            identifier = id;
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
            this.r = r;
            this.weight = weight;
            this.d = 2 * r;
            this.betweenBallsCollisions = c;
            this.betweenWallCollisions = c;
        }

        public int Identifier { get => identifier; }
        public double X0
        {
            get => x0;
            set
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
            set
            {
                if (value.Equals(y0))
                {
                    return;
                }

                y0 = value;
                RaisePropertyChanged(nameof(Y0));
            }
        }

        public double X1
        {
            get => x1;
            set
            {
                if (value.Equals(x1))
                {
                    return;
                }

                x1 = value;
            }
        }
        public double Y1
        {
            get => y1;
            set
            {
                if (value.Equals(y1))
                {
                    return;
                }

                y1 = value;
            }
        }

        public double BetweenBallsCollisions
        {
            get => betweenBallsCollisions;
            set
            {
                if (value.Equals(betweenBallsCollisions))
                {
                    return;
                }

                betweenBallsCollisions = value;
            }
        }
        public double BetweenWallCollisions
        {
            get => betweenWallCollisions;
            set
            {
                if (value.Equals(betweenWallCollisions))
                {
                    return;
                }

                betweenWallCollisions = value;
            }
        }

        public int R { get => r; }
        public int D { get => d; }
        public double Weight { get => weight; }
        public void Move(double time)
        {
        
            X0 += X1 * time;
            Y0 += Y1 * time;
            
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