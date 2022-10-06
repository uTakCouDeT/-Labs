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

    public IEnumerable<Car> YearProduct()
    {
        Cars.Sort(new CarComparer(CarComparerType.ProductionYear));
        for (int i = 0; i < Cars.Count; ++i)
        {
            yield return Cars[i];
        }
    }

    public IEnumerable<Car> Speed()
    {
        Cars.Sort(new CarComparer(CarComparerType.MaxSpeed));
        for (int i = 0; i < Cars.Count; ++i)
        {
            yield return Cars[i];
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

        CarCatalog catalog = new CarCatalog(car1, car2, car3, car4);

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

        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Проход по элементам массива с фильтром по году выпуска");
        Console.WriteLine("------------------------------------------------------");
        foreach (Car elem in catalog.YearProduct())
        {
            elem.Print();
        }

        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("Проход по элементам массива с фильтром по максимальной скорости");
        Console.WriteLine("---------------------------------------------------------------");
        foreach (Car elem in catalog.Speed())
        {
            elem.Print();
        }
    }
}