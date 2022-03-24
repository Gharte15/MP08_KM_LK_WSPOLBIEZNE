using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;


namespace TestCalculator
{
    [TestClass]
    public class CalculatorOperationsTest
    {
        [TestMethod]
        public void TestAdd()
        {
            double a = 2;
            double b = 2;

            CalculatorOperations operations = new CalculatorOperations();
            
            double c = operations.Add(a, b);    

            Assert.AreEqual(4, c);
        }

        [TestMethod]
        public void TestSubstract()
        {
            double a = 4;
            double b = 2;

            CalculatorOperations operations2 = new CalculatorOperations();
            
            double c = operations2.Substract(a, b);

            Assert.AreEqual(2, c);
        }

        [TestMethod]
        public void TestMultiply()
        {
            double a = 4;
            double b = 2;

            CalculatorOperations operations = new CalculatorOperations();

            double c = operations.Multiply(a, b);

            Assert.AreEqual(8, c);
        }

        [TestMethod]
        public void TestDivide()
        {
            double a = 8;
            double b = 2;

            CalculatorOperations operations = new CalculatorOperations();

            double c = operations.Divide(a, b);
            Assert.AreEqual(4, c);
        }

        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException))]

        public void TestDivideByZero()
        {
            double a = 4;
            double b = 0;

            CalculatorOperations operations = new CalculatorOperations();

            double c = operations.Divide(a, b);
        }


    }
}