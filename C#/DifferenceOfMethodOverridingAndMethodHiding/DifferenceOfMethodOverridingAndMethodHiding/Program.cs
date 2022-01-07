using System;

namespace DifferenceOfMethodOverridingAndMethodHiding
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape rectBase = new Rectangle();
            Square squareBase = new Square();
            Console.WriteLine($"This is rectangle with Base type:{rectBase.GetDescriptionOfShape()}");
            Console.WriteLine($"This is square with Base type:{squareBase.GetDescriptionOfShape()} ");
            squareBase.SpecialAbility();//Method hiding is use when we have a derived class that have extra method inside derived,but after update the base class(external library) with same method as derived
            //since method already exist in derived class,method hiding will force the derived function instead of based class because we need to have derived type anyway to be able to call
            //the derived method.

            //Since before we need to use derived class to be able to call added derived function, we have derived as a type not base class.

            //We always use method hiding any way when defining a new function in a derived class

            //Method hiding will depend on the type the variable is holding in.


            //Method overidding is use when we already have a base class method, but would like to change functionality in the derived class given that
            //Both contain the same name and signature, this will depend on the class assign to.
            Shape recWithBaseTypeAndAssignBase = new Shape();
            Shape recWithBaseTypeAndAssignDerive = new Rectangle();
            //Rectangle recWithDeriveTypeAndAssignBase = new Shape();//Error
            Rectangle recWithDeriveTypeAndAssignDerive = new Rectangle();
            Console.WriteLine();
            Console.WriteLine("Output from GetDescription Method");
            Console.WriteLine($"Type:Base Assign Class:Base Output:{recWithBaseTypeAndAssignBase.GetDescriptionOfShape()}");//no specific description
            Console.WriteLine($"Type:Base Assign Class:Derive Output:{recWithBaseTypeAndAssignDerive.GetDescriptionOfShape()}");//4 corners
            Console.WriteLine($"Type:Derive Assign Class:Derive Output:{recWithDeriveTypeAndAssignDerive.GetDescriptionOfShape()}");//4 corners
            //This allow covariance to occur result in passing a base type and get the desire outcome depending on the assign class.

            //Both method overidding and hiding is use so that the code follow open/close principle, so we dont have to modify existing code only extending it.
            Sedan sedan = new Sedan();
            sedan.Impress();//When we move to base class method, they do not know the existance
            //of any derived method.
            Console.WriteLine(sedan.GetFeature());//if we want it to call derived, override it.

        }
    }
    class Car
    {
        public void Impress()
        {
            Console.WriteLine($"I impress with my best feature:{GetFeature()}");
        }
        public virtual string GetFeature()
        {
            return "I move people from place to place";
        }
    }
    class Sedan : Car
    {
        public new string GetFeature()
        {
            return "I have very elegant looks";
        }
    }
}
