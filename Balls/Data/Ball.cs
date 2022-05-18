using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall : INotifyPropertyChanged
    {
        int X { get; set; }
        int Y { get; set; }
        int R { get;}
        int Weight { get; }

        int Identifier { get; }
        void Move();
        void Reset();
        void CreateTask(int period);
    }

    internal class Ball : IBall
    {
        private int x;
        private int y;
        private readonly int r;
        private readonly int weight;
        private readonly int identifier;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private Task task;
        private bool stop = false;

        public Ball(int id, int x, int y, int r, int weight)
        {
            identifier = id;
            this.x = x;
            this.y = y;
            this.r = r;
            this.weight = weight;
        }

        public int Identifier { get => identifier; }
        public int X
        {
            get => x;
            set
            {
                if (value.Equals(x))
                {
                    return;
                }

                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public int Y
        {
            get => y;
            set
            {
                if (value.Equals(y))
                {
                    return;
                }

                y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

        public int R { get => r; }
        public int Weight { get => weight; }
        public void Move()
        {
            X = x + R;
            Y = y + R;
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
                    Move();
                    RaisePropertyChanged();
                }
                stopwatch.Stop();

                await Task.Delay((int)(period - stopwatch.ElapsedMilliseconds));
            }
        }
        public void Reset()
        {
            stop = true;
        }


    }
}