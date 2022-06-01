using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BallCollisionLog
    {
        public double Ball1 { get; }
        public double Ball2 { get; }
        public BallCollisionLog(IBall ball1, IBall ball2)
        {
            Ball1 = ball1.Identifier;
            Ball2 = ball2.Identifier;
        }
    }
}
