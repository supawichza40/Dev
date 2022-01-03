using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingEqualityWithList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list1 = new List<int>();
            list1.Add(2);
            list1.Add(1);
            List<int> list2 = new List<int>();
            list2.Add(1);
            list2.Add(2);
            Console.WriteLine($"The value of two list is the same == {list1==list2}");
            Console.WriteLine($"The value of two list is the same .equal {list1.SequenceEqual(list2)}");
        }
    }
}
