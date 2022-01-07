using System;

namespace DifferenceOfMethodOverridingAndMethodHiding
{
    class Square : Shape
    {
        public new string GetDescriptionOfShape()//This is method hiding by default, but we can use the keyword new to tell that it is a method hiding.
        {
            return "This is a square with 4 corners";
        }
        public new void SpecialAbility()
        {
            Console.WriteLine("This is square specialty");
        }
    }
}
