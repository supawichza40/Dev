namespace DifferenceOfMethodOverridingAndMethodHiding
{
    class Shape
    {
        public virtual string GetDescriptionOfShape()
        {
            return "This is a generic class, no specific description";
        }
        //Adding same method in square to show method hiding
        //public virtual void SpecialAbility()
        //{
        //    Console.WriteLine("This class have no special ability");
        //}
    }
}
