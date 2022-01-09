using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            Tuple<int, int> t = new Tuple<int, int>(1, 2);
            Console.WriteLine(t);
            Console.WriteLine("Date");
            var res = new TimeSpan(10, 10,0)+new TimeSpan(10, 10, 1) + new TimeSpan(10, 10, 1);
            Console.WriteLine(res);
        }
    }
}
