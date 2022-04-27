using System.ComponentModel;
using Model;

namespace ViewModel
{
    class ViewModelBalls : INotifyPropertyChanged
    {
        private Ball ball;
        //private Ball [] balls = new Ball [Model.Ball.NumberOfBalls]

    public ViewModelBalls()
    {
        ball = new Ball(50, 50, 20);
    }

    public int X
        {
            get { return ball.X; }
            set { ball.X = value; }
        }
    public int Y
        {
            get { return ball.Y; }
            set { ball.Y = value; }
        }

    public int R
        {
            get { return ball.R; }
            set { ball.R = value; }
        }

    public Ball createBall(int x, int y, int r)
        {
            Ball ball = new Ball(x, y, r);
            return ball;
        }

    public void moveBallRandomly()
    {
        var ran = new Random();
        ball.X = ran.Next(5);
        ball.Y = ran.Next(5);
    }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}