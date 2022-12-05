using System.Globalization;
using System.Linq;
using System.Net;

public class Weather
{
    public string Country { set; get; }
    public string Name { set; get; }
    public double Temp { set; get; }
    public string Description { set; get; }

    public Weather()
    {
        Country = "";
        Name = "";
        Temp = 0;
        Description = "";
    }

    public Weather(string contry, string name, double temp, string description)
    {
        Country = contry;
        Name = name;
        Temp = temp;
        Description = description;
    }

    public Weather(string txtJson)
    {
        string tempString = ":{\"temp\":";
        int tempIndex = txtJson.IndexOf(tempString) + tempString.Length;
        Temp = double.Parse(txtJson.Substring(tempIndex, txtJson.IndexOf(",\"feels_like\":") - tempIndex),
            CultureInfo.InvariantCulture) - 273;
        Temp = Math.Round(Temp, 2);

        string descriptionString = ",\"description\":\"";
        int descriptionIndex = txtJson.IndexOf(descriptionString) + descriptionString.Length;
        Description = txtJson.Substring(descriptionIndex, txtJson.IndexOf("\",\"icon\":") - descriptionIndex);

        string countryString = "\"sys\":{\"country\":\"";
        if (txtJson.IndexOf(countryString) > -1)
        {
            int countryIndex = txtJson.IndexOf(countryString) + countryString.Length;
            Country = txtJson.Substring(countryIndex, txtJson.IndexOf("\",\"sunrise\":") - countryIndex);

            string nameString = ",\"name\":\"";
            int nameIndex = txtJson.IndexOf(nameString) + nameString.Length;
            Name = txtJson.Substring(nameIndex, txtJson.IndexOf("\",\"cod\":") - nameIndex);
        }
        else
        {
            Country = "";
            Name = "";
        }
    }

    public string ToString()
    {
        string str = "";
        if (Country != "")
        {
            str += $"{Country}, {Name}: ";
        }

        str += $"{Temp}°C ({Description})";
        return str;
    }

    public void Print()
    {
        if (Country != "")
        {
            Console.Write($"{Country}, {Name}: ");
        }

        Console.WriteLine($"{Temp}°C ({Description})");
    }
}

public enum WeatherComparerType
{
    Temp,
    Description
}

class WeatherComparer : IComparer<Weather>
{
    private readonly WeatherComparerType _weatherComparerType;

    public WeatherComparer(WeatherComparerType comparerType)
    {
        _weatherComparerType = comparerType;
    }

    public int Compare(Weather a, Weather b)
    {
        return _weatherComparerType switch
        {
            WeatherComparerType.Description => String.Compare(a.Description, b.Description, StringComparison.Ordinal),
            WeatherComparerType.Temp => a.Temp.CompareTo(b.Temp),
            _ => 0
        };
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        const string filePath = @"C:\Users\user\Documents\GitHub\csLabs\lab-6\lab-6\weathers.txt";
        const string apiKey = "94b3e2fa33ad3e53428fa6b749f38213";
        Random rnd = new Random();
        List<Weather> weathers = new List<Weather>();
        Console.WriteLine("[1] - API");
        Console.WriteLine("[2] - File");
        Console.WriteLine("[3] - API -> file");

        var flag = Console.ReadLine();

        if (flag == "1" || flag == "3")
        {
            for (int i = 0; i < 50; i++)
            {
                Weather weather = new Weather();
                string txtJson = "";

                while (weather.Country == "")
                {
                    double lat = rnd.NextDouble() * 180 - 90;
                    double lon = rnd.NextDouble() * 360 - 180;

                    string apiRequest =
                        $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiRequest);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            txtJson = reader.ReadToEnd();
                        }
                    }

                    weather = new Weather(txtJson);
                }

                if (flag == "3")
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        await writer.WriteLineAsync(txtJson);
                    }
                }

                weathers.Add(weather);
                Console.Clear();
                Console.WriteLine($"\n                     Loading {2 * (i + 1)}%");
                Console.WriteLine("||" + new string('=', i) + new string('-', 50 - i) + "||");
            }
        }

        if (flag == "2")
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    weathers.Add(new Weather(line));
                }
            }
        }

        Console.Clear();

        if (flag == "1" || flag == "2" || flag == "3")
        {
            Console.WriteLine("Вся коллекция: ");
            foreach (var item in weathers)
            {
                item.Print();
            }

            foreach (var item in weathers.OrderBy(weather => weather.Temp))
            {
                //item.Print();
            }

            Console.WriteLine("\n1. Страна с максимальной и минимальной температурой: ");
            Weather min = weathers.Min(new WeatherComparer(WeatherComparerType.Temp))!;
            Weather max = weathers.Max(new WeatherComparer(WeatherComparerType.Temp))!;
            min.Print();
            max.Print();

            Console.WriteLine(
                $"\n2. Средняя температура в мире: {Math.Round(weathers.Average(weather => weather.Temp), 2)}");

            Console.WriteLine($"\n3. Количество стран в коллекции: {weathers.Count}");

            Console.WriteLine(
                "\n4. Первая найденная страна, где Description: \"clear sky\", \"rain\", \"few clouds\": ");

            if (weathers.Any(weather => weather.Description == "clear sky"))
            {
                weathers.First(weather => weather.Description == "clear sky").Print();
            }
            else
            {
                Console.WriteLine("no description of \"clear sky\"");
            }

            if (weathers.Any(weather => weather.Description == "rain"))
            {
                weathers.First(weather => weather.Description == "rain").Print();
            }
            else
            {
                Console.WriteLine("no description of \"rain\"");
            }

            if (weathers.Any(weather => weather.Description == "few clouds"))
            {
                weathers.First(weather => weather.Description == "few clouds").Print();
            }
            else
            {
                Console.WriteLine("no description of \"few clouds\"");
            }
        }
    }
}