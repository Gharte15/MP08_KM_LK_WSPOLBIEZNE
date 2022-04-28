using System;
using System.Collections.Generic;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BallTest
{
    [TestClass]
    public class BallTest
    {
        private readonly Ball ball = new Ball(1,2,3);
        private List<Ball> balls = new List<Ball>();

        [TestMethod]
        public void TestCords()
        {
            Assert.AreEqual(ball.X, 1);
            Assert.AreEqual(ball.Y, 2);
        }
        [TestMethod]
        public void TestMove()
        {
            ball.MoveBall(100, 100);
            Assert.AreNotEqual(ball.X, 1);
            Assert.AreNotEqual(ball.Y, 2);
        }

        [TestMethod]
        public void TestAdding()
        {
            balls.Add(ball);
            Assert.AreEqual(balls.Count, 1);
        }

       
    }
}
