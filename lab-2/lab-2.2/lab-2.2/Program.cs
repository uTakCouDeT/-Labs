using System;

namespace lab_2._2
{
    class Vehicle
    {
        private double Price;
        private int Speed;
        private int Year;

        public Vehicle(double price, int speed, int year)
        {
            Price = price;
            Speed = speed;
            Year = year;
        }

        public virtual void Print()
        {
            Console.WriteLine($"Скорость: {this.Speed} \nЦена: {this.Price} \nГод выпуска: {this.Year}");
        }
    }

    class Car : Vehicle
    {
        public Car(double price, int speed, int year) : base(price, speed, year)
        {
        }

        public override void Print()
        {
            Console.WriteLine("Car:");
            base.Print();
        }
    }

    class Plane : Vehicle
    {
        private double Hight;
        private int Passengers;

        public Plane(double price, int speed, int year, double hight, int passengers) : base(price, speed, year)
        {
            Hight = hight;
            Passengers = passengers;
        }

        public override void Print()
        {
            Console.WriteLine("Plane:");
            base.Print();
            Console.WriteLine($"Высота: {this.Hight} \nКоличество пассажиров: {this.Passengers}");
        }
    }

    class Ship : Vehicle
    {
        private int Port;
        private int Passengers;

        public Ship(double price, int speed, int year, int port, int passengers) : base(price, speed, year)
        {
            Port = port;
            Passengers = passengers;
        }

        public override void Print()
        {
            Console.WriteLine("Ship:");
            base.Print();
            Console.WriteLine($"Порт приписки: {this.Port} \nКоличество пассажиров: {this.Passengers}");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Car car = new Car(1100000, 220, 2019);
            Plane plane = new Plane(38000000, 420, 2013, 5000, 120);
            Ship ship = new Ship(15900000, 160, 2005, 5, 300);
            car.Print();
            Console.WriteLine();
            plane.Print();
            Console.WriteLine();
            ship.Print();
        }
    }
}