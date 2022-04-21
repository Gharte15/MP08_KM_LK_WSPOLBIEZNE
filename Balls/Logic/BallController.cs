namespace Logic {

    public class BallController
    {
        public BallController()
        {
        }

        public Ball [] balls = new Ball[20];
        public Ball createBall(int r, int x, int y)
        {
            Ball ball = new Ball(r, x, y);
            return ball;
        }

        public void addBall(Ball ball)
        {
            balls.Append(ball);
        }

        public int changeCoordinate(int c)
        {
            Random random = new Random();
            int a = random.Next(c - 10, c + 10);
            return a;
        }

        public void moveBall(Ball ball)
        {
            ball.x = changeCoordinate(ball.x);
            ball.y = changeCoordinate(ball.y);
        }



    }
}