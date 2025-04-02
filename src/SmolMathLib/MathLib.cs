
namespace SmolMathLib
{
    /// <summary>
    /// A static class containing mathematical functions
    /// </summary>
    public static class MathLib
    {

        /// <summary>
        /// Adds the two numbers
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Sum of the two numbers</returns>
        public static double Add(double a, double b)
        {
            return a+b;
        }
        
        /// <summary>
        /// Subdivides the second number from the first
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Difference between the two numbers</returns>
        public static double Sub(double a, double b)
        {
            return a-b;
        }

        /// <summary>
        /// Multiplies the two numbers
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>Product of the two numbers</returns>
        public static double Mul(double a, double b)
        {
            return a*b;
        }

        /// <summary>
        /// Divides the dividend by the divisor
        /// </summary>
        /// <param name="a">Dividend</param>
        /// <param name="b">Divisor</param>
        /// <returns>Quotient of the operation</returns>
        public static double Div(double a, double b)
        {
            return a/b;
        }

        /// <summary>
        /// Product of all numbers smaller or equal to <c>n</c>
        /// </summary>
        /// <param name="n">Factorial input</param>
        /// <returns>Factorial of n</returns>
        public static ulong Fac(int n)
        {
            ulong factorial = 1;
            for (int i = 2; i <= n; i++)
            {
                factorial *= Convert.ToUInt64(i);
            }
            return factorial;
        }

        /// <summary>
        /// Exponentiation of <c>b</c> to the power of <c>e</c>
        /// </summary>
        /// <param name="b">Base</param>
        /// <param name="e">Exponent (power)</param>
        /// <returns>Base to the power of the exponent</returns>
        public static double Pow(double b, int e)
        {
            double result = 1;
            for(int i = 0; i < e; i++)
            {
                result *= b;
            }
            return result;
        }

        /// <summary>
        /// Nth root
        /// </summary>
        /// <param name="r">Radicand</param>
        /// <param name="d">Degree</param>
        /// <returns></returns>
        public static double Root(double r, int d)
        {            
            return Math.Pow(r, (double)1 / d);
        }

        /// <summary>
        /// Natural (base e) logarithm
        /// </summary>
        /// <param name="a">Anti-logarithm</param>
        /// <returns>Natural logarithm of b</returns>
        public static double Log(double a)
        {
            return Math.Log(a);
        }
    }

}
