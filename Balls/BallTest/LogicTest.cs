using Logic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLogic
{
    [TestClass]

    public class LogicTest
    {
        private LogicAbstractApi lLayer;
        private DataAbstractApi data;

        [TestMethod]
        public void TestApiCreation()
        {
            lLayer = LogicAbstractApi.CreateApi();
            Assert.IsNotNull(lLayer);
        }

        [TestMethod]
        public void TestWidthHeight()
        {
            data = DataAbstractApi.CreateApi();
            lLayer = LogicAbstractApi.CreateApi();
            Assert.AreEqual(Board.width, data.Width);
            Assert.AreEqual(Board.height, data.Height);

        }

        [TestMethod]
        public void TestCreateBalls()
        {
            lLayer = LogicAbstractApi.CreateApi();
            Assert.AreEqual(5, lLayer.CreateBalls(5).Count);
            
        }
    }
}
