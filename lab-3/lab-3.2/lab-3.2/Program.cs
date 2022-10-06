public class Car
{
    public string Name { set; get;}
    public string Engine { set; get;}
    private string MaxSpeed { set; get;}

    public Car(string name, string engine, string maxSpeed)
    {
        Name = name;
        Engine = engine;
        MaxSpeed = maxSpeed;
    }
    
    public override string ToString()
    {
        return Name;
    }

    public bool Equals(Car other)
    {
        return Name == other.Name && Engine == other.Engine && MaxSpeed == other.MaxSpeed;
    }
    
}

public class CarsCatalog
{
    private readonly Car[] _cars;

    public CarsCatalog(params Car[] cars)
    {
        _cars = cars;
    }
    
    public string this[uint index] => $"{_cars[index].Name} (engine: {_cars[index].Engine})";
}

internal class Program
{
    public static void Main(string[] args)
    {
        Car car1 = new("Zigul", "B230FT", "180");
        Car car2 = new("Toyota Land Cruiser 300", "V35A-FTS", "210");
        var car3 = car1;
        Console.WriteLine($"car1.Equals(car3) = {car1.Equals(car3)}");
        CarsCatalog cars = new(car1, car2, car3);
        Console.WriteLine($"cars[1]: {cars[1]}");
        Console.WriteLine($"cars[2]: {cars[2]}");
    }
}
