namespace Model
{
    public class Ball
    {
        private int x, y, r;

        public int X { get; set; }
        public int Y { get; set; }
        public int NumberOfBalls { get; set; }

        public int R { get; set; }

        public Ball(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }
}