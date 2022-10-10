class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }

    public Car(string name, int year, int speed)
    {
        Name = name;
        ProductionYear = year;
        MaxSpeed = speed;
    }

    public void Print()
    {
        Console.WriteLine($"{Name}: {ProductionYear} year - {MaxSpeed} km/h");
    }
}

public enum CarComparerType
{
    ProductionYear,
    MaxSpeed,
    Name
}

class CarComparer : IComparer<Car>
{
    private readonly CarComparerType _carComparerType;

    public CarComparer(CarComparerType comparerType)
    {
        _carComparerType = comparerType;
    }

    public int Compare(Car a, Car b)
    {
        return _carComparerType switch
        {
            CarComparerType.Name => String.Compare(a.Name, b.Name, StringComparison.Ordinal),
            CarComparerType.MaxSpeed => a.MaxSpeed.CompareTo(b.MaxSpeed),
            CarComparerType.ProductionYear => a.ProductionYear.CompareTo(b.ProductionYear),
            _ => 0
        };
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car("Zhiguli", 1990, 150);
        Car car2 = new Car("Toyota", 2018, 180);
        Car car3 = new Car("Nisan", 2006, 190);
        Car car4 = new Car("BMW", 2021, 220);
        Car car5 = new Car("Panzerkampfwagen Neubaufahrzeug", 1930, 30);

        List<Car> cars = new List<Car> { car1, car2, car3, car4, car5 };

        while (true)
        {
            Console.WriteLine("----------------------------\n" +
                              "Выберите параметр сортировки\n" +
                              " 1. По имени\n" +
                              " 2. По году производства\n" +
                              " 3. По скорости\n" +
                              " 0. Выход");

            int f = int.Parse(Console.ReadLine());

            switch (f)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Console.WriteLine("Сортировка по имени");
                    cars.Sort(new CarComparer(CarComparerType.Name));
                    break;
                case 2:
                    Console.WriteLine("Сортировка по году производства");
                    cars.Sort(new CarComparer(CarComparerType.ProductionYear));
                    break;
                case 3:
                    cars.Sort(new CarComparer(CarComparerType.MaxSpeed));
                    Console.WriteLine("Сортировка по скорости");
                    break;
                default:
                    Console.WriteLine("Выберите параметр из списка!");
                    break;
            }

            if (f == 1 || f == 2 || f == 3)
            {
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].Print();
                }
            }
        }
    }
}