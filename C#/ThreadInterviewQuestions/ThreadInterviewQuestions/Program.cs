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
            Thread t = new Thread(()=>PrintOneToHundread(10));
            t.Start();
            t.Join();
            ParameterizedThreadStart ts = new ParameterizedThreadStart(PrintOneToHundread);
            Thread t2 = new Thread(ts);
            t2.Start(10);
        }
        public static void PrintOneToHundread(object val)
        {
            for (int i = 0; i < ((int)val); i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
