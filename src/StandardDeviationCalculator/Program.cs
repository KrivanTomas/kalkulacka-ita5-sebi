using SmolMathLib;

namespace StandardDeviationCalculator
{
    public static class StandardDeviationCalculator
    {
        public static int Main()
        {
            int eofChar = -1;

            double sum = 0;
            double sumOfPower2 = 0;
            int numCount = 0;

            // reading variables
            int readChar = Console.Read();
            double readNum = 0;
            bool lastWhitespace = true;
            bool setNegative = false;

            while (true)
            {
                // character '-1' is eof
                if(!Char.IsWhiteSpace((char)readChar) && readChar != eofChar) {
                    if ((readChar < '0' || readChar > '9') && readChar != '-')
                    {
                        Console.WriteLine("Error: input not number. Exiting\n\r");
                        return 1;
                    }
                    // input is <0,9> or -

                    if (readNum == 0 && readChar == '-') {
                        setNegative = true;
                    }
                    else if(readChar == '-'){
                        // '-' character is inside a number f.e. 15-753
                        Console.WriteLine("Error: \'-\' character inside number. Exiting\n\r");
                        return 1;
                    }
                    else {
                        // load number
                        readNum = MathLib.Add(readNum * 10, readChar - '0');
                    }

                    lastWhitespace = false;
                }
                else if (!lastWhitespace)
                {
                    if (setNegative)
                        readNum *= -1;

                    sum = MathLib.Add(sum, readNum);
                    sumOfPower2 = MathLib.Add(sumOfPower2, MathLib.Pow(readNum, 2));
                    readNum = 0;
                    numCount++;
                    lastWhitespace = true;
                    setNegative = false;
                }
                if (readChar == eofChar) //EOF
                    break;
                readChar = Console.Read();
            }

            if (numCount == 0)
            {
                Console.Write("Error: No input given. Exiting\n\r");
                return 1;
            }

            //úprava vzorců: N*(xavg)^2 <=> (N*(xsum)^2)/(N^2) <=> (xsum^2)/N
            double s = CalculateStandardDeviation(sum, sumOfPower2, numCount);
            Console.WriteLine(s);
            return 0;
        }

        /**
         * <summary> Calculates the Standard Deviation from sums of numbers </summary>
         * <param name="sum">Sum of all numbers</param> 
         * <param name="powerSum">Sum of powers of 2 of all numbers individually</param>
         * <param name="n">Count of numbers</param>
         * <returns>Resulting Standard Deviation of type double</returns>
         */
        public static double CalculateStandardDeviation(double sum, double powerSum, int n)
        {
            double avg = MathLib.Div(MathLib.Pow(sum, 2), n);
            return MathLib.Root(MathLib.Div(MathLib.Sub(powerSum, avg), n - 1), 2);
        }
    } //end class
} //end namespace

