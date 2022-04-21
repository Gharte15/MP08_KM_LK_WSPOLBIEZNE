namespace Data
{
    public class Ball
    {
        public int r { get; }
        public int x { get; set }
        public int y { get; set }

        public Ball(int r, int x, int y)
        {
            this.r = r;
            this.x = x;
            this.y = y;
        }


    }
}