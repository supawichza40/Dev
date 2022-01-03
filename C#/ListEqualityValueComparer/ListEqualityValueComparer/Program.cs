using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListEqualityValueComparer
{
    /// <summary>
    /// This program is for modifying the List object, since when we want to compare the value in both two list with different
    /// reference address, it always return false even when values are the same.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Test that two list with different reference but same data, return false.
            List<int> listOne = new List<int> { 1, 2, 3, 4 };
            List<int> listTwo = new List<int> { 1, 2, 3, 4 };

            Console.WriteLine($"Using == operator return {listOne==listTwo}");//return False
            Console.WriteLine($"Using .equals operator return {listOne.Equals(listTwo)}");//return False
            //1.
            //General answer is to sort both list and use SequenceEqual to compare value.
            //listOne.SequenceEqual
            //2.By create our own list version that override the .Equals method to compare value instead of a reference address.
            SupavichList<int> listthree = new SupavichList<int> { 1, 2, 3, 4 };
            SupavichList<int> listfour = new SupavichList<int> { 1, 2, 3, 4 };

            Console.WriteLine($"Using == operator return {listthree == listfour}");//return False
            Console.WriteLine($"Using .equals operator return {listthree.Equals(listfour)}");//return true


        }
    }
    class SupavichList<T> : List<T>
    {
        public override bool Equals(object obj)
        {
            if (obj==null || this == null)
            {
                return false;
            }
            if (this.Count > (obj as List<T>).Count)
            {
                return false;
            }
            else if (this.Count < (obj as List<T>).Count)
            {
                return false;
            }
            else
            {
                List<T> temp = obj as List<T>;
                for (int i = 0; i < (obj as List<T>).Count; i++)
                {
                    if (!this.Contains(temp[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
