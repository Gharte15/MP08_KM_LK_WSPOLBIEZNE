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
            LogicLayer = LogicAbstractApi.CreateApi(width, height);
            width = Width;
            height = Height;
            LogicLayer.Update += (sender, args) => Move();
        }

        public override IList ModelBalls(int ballNumber) => LogicLayer.CreateBallsList(ballNumber);
        

        public override void Move()
        {

        }

        public override void Stop()
        {
          
        }
    }

}
