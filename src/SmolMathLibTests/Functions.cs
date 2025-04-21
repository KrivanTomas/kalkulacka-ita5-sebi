using SmolMathLib;

namespace SmolMathLibTests
{
    class Functions
    {
        Utils util;
        double delta = 0.00000001d;
        int maxFactorialTest = 22;
        int randomIterations = 10;

        [SetUp]
        public void Setup()
        {
            util = new Utils();
        }

        [Test]
        public void FactorialTest()
        {
            ulong n = 1;
            for(int i = 0; i <= maxFactorialTest; i++, n*=Convert.ToUInt64(i))
            {
                Assert.That(MathLib.Fac(i), Is.EqualTo(n));
                Console.WriteLine(i + ": " + n);
            }
        }

        [Test]
        public void PowAxiom1()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double result = MathLib.Pow(a, 0);
                Assert.That(result, Is.EqualTo(1d).Within(delta));
            }
        }

        [Test]
        public void PowAxiom2()
        {
            for (int i = 0; i < randomIterations; i++)
            {
                double a = util.RandomDouble();
                double result = MathLib.Pow(a, 1);
                Assert.That(result, Is.EqualTo(a).Within(delta));
            }
        }

        [TestCase(2d, 0, 1d)]
        [TestCase(2d, 1, 2d)]
        [TestCase(2d, 2, 4d)]
        [TestCase(2d, 3, 8d)]
        [TestCase(2d, 4, 16d)]
        [TestCase(2d, 5, 32d)]
        [TestCase(2d, 6, 64d)]
        [TestCase(-10d, 2, 100d)]
        [TestCase(-10d, 3, -1000d)]
        [TestCase(-3d, 9, -19683d)]
        [TestCase(24d, 4, 331776d)]
        [TestCase(45d, 3, 91125d)]
        [TestCase(2.78d, 5, 166.0443030367d)]
        [TestCase(0.5d, 10, 0.0009765625d)]
        public void PowTest(double a, int e, double expected)
        {
            double result = MathLib.Pow(a, e);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(1d, 2, 1d)]
        [TestCase(1d, 4, 1d)]
        [TestCase(1d, 100, 1d)]
        [TestCase(8d, 2, 2.8284271247461900976033774484194d)]
        [TestCase(144d, 2, 12d)]
        [TestCase(324234d, 10, 3.5570163005790818833365111811645d)]
        [TestCase(423d, 7, 2.3724194782191462630462054470579d)]
        public void RootTest(double a, int b, double expected)
        {
            double result = MathLib.Root(a, b);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }

        [TestCase(1d, 0d)]
        [TestCase(34d, 3.52636052462d)]
        [TestCase(4d, 1.38629436112d)]
        [TestCase(5.7d, 1.74046617484d)]
        [TestCase(15034d, 9.61806958174d)]
        [TestCase(0.5d, -0.69314718056d)]
        [TestCase(0.000042d, -10.0778409397d)]
        [TestCase(0.001d, -6.90775527898d)]
        [TestCase(0.4d, -0.916290731874d)]
        public void LogTest(double a, double expected)
        {
            double result = MathLib.Log(a);
            Assert.That(result, Is.EqualTo(expected).Within(delta));
        }
    }
}
