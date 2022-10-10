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

class CarCatalog
{
    public List<Car> Cars = new List<Car>();

    public CarCatalog(params Car[] cars)
    {
        for (int i = 0; i < cars.Length; ++i)
        {
            Cars.Add(cars[i]);
        }
    }

    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < Cars.Count; ++i)
        {
            yield return Cars[i];
        }
    }

    public IEnumerable<Car> Reverse()
    {
        for (int i = Cars.Count - 1; i >= 0; --i)
        {
            yield return Cars[i];
        }
    }

    public IEnumerable<Car> YearProduct(int year)
    {
        for (int i = 0; i < Cars.Count; ++i)
        {
            if (Cars[i].ProductionYear <= year)
            {
                yield return Cars[i];
            }
        }
    }

    public IEnumerable<Car> Speed(int speed)
    {
        for (int i = 0; i < Cars.Count; ++i)
        {
            if (Cars[i].MaxSpeed >= speed)
            {
                yield return Cars[i];
            }
        }
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


        CarCatalog catalog = new CarCatalog(car1, car2, car3, car4, car5);

        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Прямой проход с первого элемента до последнего");
        Console.WriteLine("----------------------------------------------");
        foreach (Car elem in catalog)
        {
            elem.Print();
        }

        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Обратный проход от последнего к первому");
        Console.WriteLine("---------------------------------------");
        foreach (Car elem in catalog.Reverse())
        {
            elem.Print();
        }

        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Проход по элементам массива с фильтром до 2000 года выпуска");
        Console.WriteLine("-----------------------------------------------------------");
        foreach (Car elem in catalog.YearProduct(2000))
        {
            elem.Print();
        }

        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Проход по элементам массива с фильтром от максимальной скорости 170");
        Console.WriteLine("-------------------------------------------------------------------");
        foreach (Car elem in catalog.Speed(170))
        {
            elem.Print();
        }
    }
}