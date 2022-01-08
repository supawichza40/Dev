using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosePrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle t1 = new Triangle(10, 10);
            Square s1 = new Square.SquareBuilder().WithHeight(10).WithWidth(10).Build();//With builder class easy to read.
            //Not follow open-close principle class.
            Console.WriteLine(AreaCalculator.CalculateArea(t1));
            //follow open-closed principle class
            Console.WriteLine(AreaCalculator2.CalculateArea(t1));
        }
    }
    abstract class Shape
    {
        public int NumberOfSides { get; set; }
        public Shape(int numberOfSides)
        {
            NumberOfSides = numberOfSides;
        }
        public abstract double CalculateArea();
    }
    class Square : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        private Square(double width, double height, int numberOfSides=4) : base(numberOfSides)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()//Overriding CalculateArea since each shape implement area differently.
        {
            return Width * Height;
        }
        //We just add builder class for readability
        public class SquareBuilder
        {
            private double _width;
            private double _height;
            public SquareBuilder WithWidth(double width)
            {
                _width = width;
                return this;
            }
            public SquareBuilder WithHeight(double height)
            {
                _height = height;
                return this;
            }
            public Square Build()
            {
                return new Square(_width, _height);
            }
        }
        ///End of builder class.
    }
    class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public Triangle(double bas,double height,int numberOfSides=3) : base(numberOfSides)
        {
            Base = bas;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Base * Height * 0.5;
        }
    }
    /// <summary>
    /// This class does not follow Open-closed principle since if in the future new class is added, then we have to modify the 
    /// AreaCalculator class.
    /// </summary>
    class AreaCalculator
    {
        public static double CalculateArea(Triangle t)
        {
            return t.Base * t.Height * 0.5;
        }
        public static double CalculateArea(Square s)
        {
            return s.Width * s.Height;
        }
    }
    /// <summary>
    /// This follow the open-closed principle since now we have abstract class called shape that implement CalculateArea
    /// All class that inherit from Shape class will contain CalculateArea method, so now we do not need to change
    /// this AreaCalculator2 class anymore, and in the future new class added, we just add new shape class inherit from Shape.
    /// </summary>
    class AreaCalculator2
    {
        public static double CalculateArea(Shape s)
        {
            return s.CalculateArea();
        }
    }
}
