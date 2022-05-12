using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public abstract void Start();
        public abstract void Stop();
        //public abstract int GetCount { get; }

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

        private List<Task> tasks;
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

        public override async void UpdateBalls()
        {
            await Task.Delay(30);
            data.UpdateBalls();
        }

        public int Tasks
        {
            get => tasks.Count;
        }
        public override void Start()
        {
            tasks.Add(Task.Run(() => UpdateBalls()));
        }

        public override void Stop()
        {
            
        }
    }
}