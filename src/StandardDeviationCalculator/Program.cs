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
            int readChar; //input char
            int readNum = 0;
            double sum = 0;
            int numCount = 0;
            double s = 0; //Standard deviation
            readChar = Console.Read();

            while(true)
            {
                if ((readChar < '0' || readChar > '9') && !Char.IsWhiteSpace((char)readChar) && readChar != '\0')
                {
                    Console.WriteLine("Error: input not number. Exiting\n\r");
                    return 0;
                }
                if (!Char.IsWhiteSpace((char)readChar) && readChar != '\0')
                {
                    readNum = readNum * 10 + (readChar - '0');
                }
                else
                {
                    //full number read
                    sum += readNum;
                    readNum = 0;
                    numCount++;

                }
                
                if (readChar == '\0') //EOF
                    break;
                readChar = Console.Read();
            }
            Console.WriteLine("Směrodatná odchylka: " + sum);
            return 0;
        }

        public static void Func(int cislo)
        {
            Console.WriteLine((char)cislo);
            return;
        }
    }

}

