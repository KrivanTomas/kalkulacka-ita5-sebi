// See https://aka.ms/new-console-template for more information
// To-do: 
//  SDC (Standard deviation calculator)
//  Profiling

using SmolMathLib;

namespace StandardDeviationCalculator
{
    public static class StandardDeviationCalculator
    {

        public static int Main()
        {
            double sum = 0;
            double sumOfPower2 = 0;
            int numCount = 0;

            // reading variables
            int readChar = Console.Read();
            double readNum = 0;
            bool decimalPresent = false;
            bool lastWhitespace = true;
            while (true)
            {
                if ((readChar < '0' || readChar > '9') && !Char.IsWhiteSpace((char)readChar) && readChar != -1)
                {
                    Console.WriteLine("Error: input not number. Exiting\n\r");
                    return 1;
                }
                if (!Char.IsWhiteSpace((char)readChar) && readChar != -1)
                {
                    //if(readChar == '.')
                    readNum = MathLib.Add(readNum * 10, readChar - '0');
                    lastWhitespace = false;
                }
                else if (!lastWhitespace)
                {
                    sum = MathLib.Add(sum, readNum);
                    sumOfPower2 = MathLib.Add(sumOfPower2, MathLib.Pow(readNum, 2));
                    readNum = 0;
                    numCount++;
                    lastWhitespace = true;
                }

                if (readChar == -1) //EOF
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

