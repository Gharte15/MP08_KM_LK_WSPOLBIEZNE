using Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract int height { get; }
        public abstract int width { get; }

        public abstract IList ModelBalls(int ballNumber);
        public abstract void Move();

        public abstract void Stop();

        public abstract int getX(int i);
        public abstract int getY(int i);
        public abstract int getSize(int i);


        public static ModelAbstractApi CreateApi(int Height, int Width)
        {
            return new ModelApi(Height, Width);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        public override int width { get; }
        public override int height { get; }

        private LogicAbstractApi LogicLayer;
        public ModelApi(int Height, int Width)
        {
            LogicLayer = LogicAbstractApi.CreateApi();
            width = Width;
            height = Height;
            
        }

        public override IList ModelBalls(int ballNumber) => LogicLayer.CreateBallsList(ballNumber);

        public override int getX(int i)
        {
            return LogicLayer.getXcoordinate(i);
        }
        public override int getY(int i)
        {
            return LogicLayer.getYcoordinate(i);
        }
        public override int getSize(int i)
        {
            return LogicLayer.getSize(i);  
        }
        public override void Move()
        {
            LogicLayer.Start();
        }

        public override void Stop()
        {
          LogicLayer.Stop();
        }
    }

}
