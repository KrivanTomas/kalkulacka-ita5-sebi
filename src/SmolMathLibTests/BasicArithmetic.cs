using SmolMathLib;

namespace SmolMathLibTests
{
    public class BasicArithmetic
    {
        Utils util;
        double delta = 0.000000001d;
        int randomIterations = 10;

        [SetUp]
        public void Setup()
        {
            util = new Utils();
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        // Add tests

        [Test]
        public void AddCommutativity()
        {
            for(int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double b = util.RandomDouble();
                double result1 = MathLib.Add(a, b);
                double result2 = MathLib.Add(b, a);
                Assert.That(result1, Is.EqualTo(result2).Within(delta));
            }
        }

        [Test]
        public void AddAssociativity()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double b = util.RandomDouble();
                double c = util.RandomDouble();
                double result1 = MathLib.Add(a, MathLib.Add(b, c));
                double result2 = MathLib.Add(b, MathLib.Add(a, c));
                Assert.That(result1, Is.EqualTo(result2).Within(delta * 100000));
            }
        }

        [TestCase(1d, 1d, 2d)]
        [TestCase(0.1d, 0.2d, 0.3d)]
        [TestCase(124798d, 5356d, 130154d)]
        public void AddPositive(double a, double b, double expected)
        {
            double result = MathLib.Add(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(1d, -1d, 0d)]
        [TestCase(-0.1d, 0.2d, 0.1d)]
        [TestCase(-1001d, 20d, -981d)]
        [TestCase(-213d, -89d, -302d)]
        public void AddNegative(double a, double b, double expected)
        {
            double result = MathLib.Add(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(213d, double.NaN, double.NaN)]
        [TestCase(1d, double.PositiveInfinity, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, 120d, double.NegativeInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
        [TestCase(double.NegativeZero, 1d, 1d)]
        [TestCase(double.NegativeZero, 0d, double.NegativeZero)]
        [TestCase(double.NegativeZero, double.NegativeZero, double.NegativeZero)]
        public void AddSpecial(double a, double b, double expected)
        {
            double result = MathLib.Add(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        // Sub tests

        [TestCase(1d, 1d, 0d)]
        [TestCase(0.1d, 0.2d, -0.1d)]
        [TestCase(124798d, 5356d, 119442d)]
        public void SubPositive(double a, double b, double expected)
        {
            double result = MathLib.Sub(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(1d, -1d, 2d)]
        [TestCase(-0.1d, 0.2d, -0.3d)]
        [TestCase(-1001d, 20d, -1021d)]
        [TestCase(-213d, -89d, -124d)]
        public void SubNegative(double a, double b, double expected)
        {
            double result = MathLib.Sub(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }
        
        [TestCase(213d, double.NaN, double.NaN)]
        [TestCase(1d, double.PositiveInfinity, double.NegativeInfinity)]
        [TestCase(double.NegativeInfinity, 120d, double.NegativeInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NaN)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.NaN)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity)]
        [TestCase(double.NegativeZero, 1d, -1d)]
        [TestCase(0d, double.NegativeZero, 0d)]
        [TestCase(double.NegativeZero, double.NegativeZero, double.NegativeZero)]
        public void SubSpecial(double a, double b, double expected)
        {
            double result = MathLib.Sub(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        // Mul tests

        [Test]
        public void MulAxiom1()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double result = MathLib.Mul(a, 0d);
                Assert.That(result, Is.EqualTo(0d).Within(delta));
            }
        }

        [Test]
        public void MulAxiom2()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double result = MathLib.Mul(a, 1d);
                Assert.That(result, Is.EqualTo(a).Within(delta));
            }
        }


        [Test]
        public void MulCommutativity()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double b = util.RandomDouble();
                double result1 = MathLib.Mul(a, b);
                double result2 = MathLib.Mul(b, a);
                Assert.That(result1, Is.EqualTo(result2).Within(delta));
            }
        }


        [Test]
        public void MulAssociativity()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.SmallerRandomDouble(1000);
                double b = util.SmallerRandomDouble(1000);
                double c = util.SmallerRandomDouble(1000);
                double result1 = MathLib.Mul(a, MathLib.Mul(b, c));
                double result2 = MathLib.Mul(b, MathLib.Mul(a, c));
                // May randomly fail due to floating point precision error with large numbers?
                Assert.That(result1, Is.EqualTo(result2).Within(delta * 100000));
            }
        }

        [TestCase(1d, 1d, 1d)]
        [TestCase(1d, 3d, 3d)]
        [TestCase(0.5d, 0.2d, 0.1d)]
        [TestCase(124798d, 5356d, 668418088d)]
        public void MulPositive(double a, double b, double expected)
        {
            double result = MathLib.Mul(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(1d, -1d, -1d)]
        [TestCase(-10d, 2d, -20d)]
        [TestCase(-1001d, 20d, -20020d)]
        [TestCase(-213d, -89d, 18957d)]
        public void MulNegative(double a, double b, double expected)
        {
            double result = MathLib.Mul(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(213d, double.NaN, double.NaN)]
        [TestCase(1d, double.PositiveInfinity, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, 120d, double.NegativeInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NegativeInfinity)]
        [TestCase(double.NegativeZero, 1d, double.NegativeZero)]
        [TestCase(0d, double.NegativeZero, 0d)]
        [TestCase(double.NegativeZero, double.NegativeZero, double.NegativeZero)]
        public void MulSpecial(double a, double b, double expected)
        {
            double result = MathLib.Mul(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        // Div tests

        [Test]
        public void DivAxiom1()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double result = MathLib.Div(a, 1d);
                Assert.That(result, Is.EqualTo(a).Within(delta));
            }
        }

        [Test]
        public void DivAxiom2()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomNonZeroDouble();
                double result = MathLib.Div(0d, a);
                Assert.That(result, Is.EqualTo(0d).Within(delta));
            }
        }

        [TestCase(1d, 1d, 1d)]
        [TestCase(3d, 1d, 3d)]
        [TestCase(10d, 0.2d, 50d)]
        [TestCase(124798d, 5356d, 23.3005974608d)]
        public void DivPositive(double a, double b, double expected)
        {
            double result = MathLib.Div(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(1d, -1d, -1d)]
        [TestCase(-10d, 2d, -5d)]
        [TestCase(-20d, 101d, -0.19801980198d)]
        [TestCase(-213d, -89d, 2.39325842697d)]
        public void DivNegative(double a, double b, double expected)
        {
            double result = MathLib.Div(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(213d, double.NaN, double.NaN)]
        [TestCase(1d, double.PositiveInfinity, 0d)]
        [TestCase(double.NegativeInfinity, 120d, double.NegativeInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, double.NaN)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, double.NaN)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, double.NaN)]
        [TestCase(double.NegativeZero, 1d, double.NegativeZero)]
        [TestCase(0d, double.NegativeZero, double.NaN)]
        [TestCase(double.NegativeZero, double.NegativeZero, double.NaN)]
        public void DivSpecial(double a, double b, double expected)
        {
            double result = MathLib.Div(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }
    }
}