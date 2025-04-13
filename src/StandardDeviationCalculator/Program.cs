// To-do: 
//  Profiling
// Additional:
//  Decimal numbers

using SmolMathLib;

namespace StandardDeviationCalculator
{
    public static class StandardDeviationCalculator
    {
        public static int Main()
        {
            int eofChar = -1; // mainly for testing purposes, indicates EOF character; default=-1
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
                    // input is {<0,9>; -}

                    if (readNum == 0 && readChar == '-') {
                        setNegative = true;
                    }
                    else if(readChar == '-'){
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
            double avg = MathLib.Div(MathLib.Pow(sum, 2), numCount);
            double s = MathLib.Root(MathLib.Div(MathLib.Sub(sumOfPower2, avg), numCount - 1), 2);
            Console.WriteLine(s);

            return 0;
        }
    }

}

