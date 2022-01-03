using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateRealWorldExample
{
    class Program
    {
        static void Main(string[] args)
        {

            Car2 c1 = new Car2("Slog", 100, 10);

            //This is doing the same as passing the delegate
            //c1.OnAboutToBlow(CarAboutToBlow);
            //c1.OnAboutToBlow(CarAboutToBlow2);

            //Same Behaviour as passing the method itself.
            c1.AddAboutToBlowMethod(new Car2.AboutToBlow(CarAboutToBlow));
            c1.AddAboutToBlowMethod(new Car2.AboutToBlow(CarAboutToBlow2));

            //How to remove passing delegate
            Car2.AboutToBlow d = new Car2.AboutToBlow(CarAboutToBlow);
            //Remove the delegate
            c1.RemoveAboutToBlowMethod(d);

            //Passing a method instead of a delegate.
            c1.AddAboutToBlowMethod(CarAboutToBlow);
            c1.AddAboutToBlowMethod(CarAboutToBlow2);
            c1.AddExplodeMethod(CarExploded);
            for (int i = 0; i < 7; i++)
            {
                c1.Accelerate(20);

            }



        }
        public static void CarAboutToBlow(string msg)
        { Console.WriteLine(msg); }
        public static void CarAboutToBlow2(string msg)
        {
            Console.WriteLine($"I also blow {msg}");
        }
        public static void CarExploded(string msg)
        { Console.WriteLine(msg); }
    }
}
