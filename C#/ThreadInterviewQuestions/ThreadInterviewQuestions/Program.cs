using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInterviewQuestions
{
    class Program
    {
        static void Main(string[] args)
        {
            //How to pass parameter to a function when using multithreading
            //1.Passing value inside thread parameter
            Thread t = new Thread(()=>PrintOneToHundread(10));
            t.Start();
            t.Join();
            //2.Using ParameterizedThreadStart and pass it at start.
            ParameterizedThreadStart ts = new ParameterizedThreadStart(PrintOneToHundread);
            Thread t2 = new Thread(ts);
            t2.Start(10);

            ThreadStart tstart = new ThreadStart(PrintGeneralOneToHundread);//Can only be use without parameter.
        }
        public static void PrintOneToHundread(object val)
        {
            for (int i = 0; i < ((int)val); i++)
            {
                Console.WriteLine(i);
            }
        }
        public static void PrintGeneralOneToHundread()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
