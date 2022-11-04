public class Weather
{
    public string Country { set; get; }
    public string Name { set; get; }
    private string Temp { set; get; }
    private string Description { set; get; }

    public Weather(string contry, string name, string temp, string description)
    {
        Country = contry;
        Name = name;
        Temp = temp;
        Description = description;
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
        Console.WriteLine("Hello");
    }
}