namespace SmolMathLibTests
{
    class Utils
    {
        Random rnd;
        double randomMax;
        double randomOffset;

        public Utils(double randomMax = 10000000d, double randomOffset = -500000d)
        {
            rnd = new Random();
            this.randomMax = randomMax;
            this.randomOffset = randomOffset;
        }

        public double RandomDouble()
        {
            return rnd.NextDouble() * randomMax + randomOffset;
        }

        public double RandomNonZeroDouble()
        {
            double a = rnd.NextDouble() * randomMax + randomOffset;
            return a != 0d ? a : 1d;
        }

        public double SmallerRandomDouble(double ratio)
        {
            return rnd.NextDouble() * randomMax / ratio + randomOffset / ratio;
        }
    }
}
