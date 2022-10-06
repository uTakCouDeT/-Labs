using System;

namespace lab_1._3
{
    public class Point
    {
        private double _x;
        private double _y;

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }
    }

    public class Figure
    {
        private Point[] _points;

        public string Name { get; }

        public Figure(int n, string name)
        {
            Name = name;
            _points = new Point[n];
            if (n >= 3 && n <= 5)
            {
                for (int i = 0; i < n; ++i)
                {
                    Console.WriteLine($"Point {i + 1}:");
                    var x = Convert.ToDouble(Console.ReadLine());
                    var y = Convert.ToDouble(Console.ReadLine());
                    _points[i] = new Point(x, y);
                }
            }
            else
            {
                Console.WriteLine("Enter 3 to 5 points. \n");
            }
        }

        double LengthSide(Point A, Point B)
        {
            double length = Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
            return length;
        }

        public void PerimeterCalculator()
        {
            double perimeter = 0;
            for (int i = 0; i < _points.Length - 1; ++i)
            {
                perimeter += LengthSide(_points[i], _points[i + 1]);
            }

            perimeter += LengthSide(_points[_points.Length - 1], _points[0]);
            Console.WriteLine($"The perimeter of the \"{Name}\" figure is {perimeter}.");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Quantity of points: ");
            var n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter figure name: ");
            string name = Console.ReadLine();
            Figure figure = new Figure(n, name);
            Console.WriteLine($"Figure name is \"{figure.Name}\".");
            figure.PerimeterCalculator();
        }
    }
}