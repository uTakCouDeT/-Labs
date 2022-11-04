public class Weather
{
    public string Country { set; get; }
    public string Name { set; get; }
    private double Temp { set; get; }
    private string Description { set; get; }

    public Weather(string contry, string name, double temp, string description)
    {
        Country = contry;
        Name = name;
        Temp = temp;
        Description = description;
    }

    public void Print()
    {
        Console.WriteLine($"{Country}, {Name}: {Temp}°C ({Description})");
    }

    public bool Equals(Weather other)
    {
        return Country == other.Country && Name == other.Name && Temp == other.Temp && Description == other.Description;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Weather weather = new Weather("Россия", "Москва", 7, "солнечно");
        weather.Print();
    }
}