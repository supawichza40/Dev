using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        delegate int listOfMethods(string input);
        public static int Add(string input)
        {
            Console.WriteLine($"I am {input}");
            return 100;
        }
        public static int Add2(string input) 
        {
            Console.WriteLine("I am again");
            return 100;
        }
        static void Main(string[] args)
        {
            listOfMethods del1 = new listOfMethods(Add);
            del1 += Add2;
            
            del1("hello");

            foreach (var item in del1.GetInvocationList())
            {
                Console.WriteLine(item.Method);
            }
            del1 -= Add;
            del1("hello");
            del1.Invoke("hello");
        }
    }
}
