using System;

namespace lab_1._2
{
    public class Rectangle
    {
        private double side1;
        private double side2;

        public Rectangle(double sideA, double sideB)
        {
            side1 = sideA;
            side2 = sideB;
        }

        double AreaCalculator()
        {
            return side1 * side2;
        }

        double PerimeterCalculator()
        {
            return (side1 + side2) * 2;
        }

        public double Area
        {
            get { return AreaCalculator(); }
        }

        public double Perimetr
        {
            get { return PerimeterCalculator(); }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the sides: ");
            double a = Convert.ToDouble(Console.ReadLine());
            double b = Convert.ToDouble(Console.ReadLine());
            var rectangle = new Rectangle(a, b);
            Console.WriteLine($"Perimetr: {rectangle.Perimetr} \nArea: {rectangle.Area}");
        }
    }
}