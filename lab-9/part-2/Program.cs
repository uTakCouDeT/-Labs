using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

public class Weather
{
    public string Country { set; get; }
    public string Name { set; get; }
    public string RuName { set; get; }
    public double Temp { set; get; }
    public string Description { set; get; }

    public Weather()
    {
        Country = "";
        Name = "";
        RuName = "";
        Temp = 0;
        Description = "";
    }

    public Weather(string txtJson)
    {
        RuName = "";
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

    public void RuPrint()
    {
        Console.WriteLine($"{RuName}: {Temp}°C ({Description}) - {Name}");
    }
}

public enum WeatherComparerType
{
    Temp,
    Description
}

class City
{
    public string Name { set; get; }
    public double Lat { set; get; }
    public double Lon { set; get; }

    public City(string name, string lat, string lon)
    {
        Name = name;
        Lat = ParseToDobleWithDot(lat);
        Lon = ParseToDobleWithDot(lon);
    }


    double ParseToDobleWithDot(string str)
    {
        double db;
        var splitString = str.Split(".");
        if (splitString.Length == 1)
        {
            db = double.Parse(splitString[0]);
        }
        else
        {
            db = double.Parse(splitString[0] + "," + splitString[1]);
        }

        return db;
    }
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
        const string filePath = @"C:\Users\user\Documents\GitHub\csLabs\lab-9\part-2\weathers.txt";
        const string cityPath = @"C:\Users\user\Documents\GitHub\csLabs\lab-9\part-2\city.txt";

        Console.WriteLine("[1] - API");
        Console.WriteLine("[2] - File");
        Console.WriteLine("[3] - API -> file");

        var saveSettings = Console.ReadLine();
        // var saveSettings = "2";


        async Task<List<City>> ParseCitiesFromFile(string cityPath)
        {
            List<City> cities = new List<City>();
            using (StreamReader reader = new StreamReader(cityPath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    cities.Add(new City(line.Split('\t')[0],
                        line.Split('\t')[1].Split(',')[0],
                        line.Split('\t')[1].Split(',')[1]));
                }
            }

            return cities;
        }


        async Task<Weather> GetWeatherFromApi(double lat, double lon, bool saveJson = false)
        {
            string txtJson;
            const string apiKey = "94b3e2fa33ad3e53428fa6b749f38213";
            string apiRequest = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiRequest);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader jsonReader = new StreamReader(stream))
                {
                    txtJson = jsonReader.ReadToEnd();
                }
            }

            if (saveJson)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync(txtJson);
                }
            }
            
            return new Weather(txtJson);
        }


        async Task<Weather> GetWeatherFromFile(double lat, double lon)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(line);
                    var coord = jsonElement.GetProperty("coord");
                    if (coord.GetProperty("lat").GetDouble() == lat && coord.GetProperty("lon").GetDouble() == lon)
                    {
                        return new Weather(line);
                    }
                }
            }

            return new Weather();
        }


        async Task SaveApiToFile()
        {
            int k = 0;
            List<City> cities = await ParseCitiesFromFile(cityPath);

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                await writer.WriteAsync("");
            }

            foreach (var city in cities)
            {
                await GetWeatherFromApi(city.Lat, city.Lon, saveJson: true);
                Console.Clear();
                Console.WriteLine("\n         Loading");
                Console.WriteLine("||" + new string('-', k % 20) + "=" + new string('-', 20 - (k % 20)) + "||");
                ++k;
            }
        }

        if (saveSettings == "3")
        {
            await SaveApiToFile();
        }

        Console.Clear();

        if (saveSettings == "1" || saveSettings == "2")
        {
            Task<Weather> GetWeather(double lat, double lon)
            {
                if (saveSettings == "1")
                {
                    return GetWeatherFromApi(lat, lon);
                }

                if (saveSettings == "2")
                {
                    return GetWeatherFromFile(lat, lon);
                }

                return null;
            }

            List<City> cities = await ParseCitiesFromFile(cityPath);
            var flag = "";
            while (flag != "0")
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("[1] - Вывести список городов");
                Console.WriteLine("[2] - Вывести погоду по названию города");
                Console.WriteLine("[3] - Вывести погоду по номеру в списке");
                Console.WriteLine("[0] - Выход");
                Console.WriteLine("---------------------------------------");

                flag = Console.ReadLine();
                switch (flag)
                {
                    case "1":
                    {
                        Console.WriteLine("Cписок городов: ");
                        for (int i = 0; i < cities.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {cities[i].Name}");
                        }

                        break;
                    }
                    case "2":
                    {
                        Console.WriteLine("Введите название города: ");
                        var name = Console.ReadLine();
                        bool isValid = false;
                        foreach (var city in cities)
                        {
                            if (city.Name == name)
                            {
                                Weather weather = await GetWeather(city.Lat, city.Lon);
                                weather.RuName = city.Name;
                                weather.RuPrint();
                                isValid = true;
                                break;
                            }
                        }

                        if (!isValid)
                        {
                            Console.WriteLine("Город не найден!");
                        }

                        break;
                    }
                    case "3":
                    {
                        Console.WriteLine("Введите номер города: ");
                        var index = Console.ReadLine();
                        bool isValid = false;
                        for (var i = 0; i < cities.Count; i++)
                        {
                            var city = cities[i];
                            if ($"{i + 1}" == index)
                            {
                                Weather weather = await GetWeather(city.Lat, city.Lon);
                                weather.RuName = city.Name;
                                weather.RuPrint();
                                isValid = true;
                                break;
                            }
                        }

                        if (!isValid)
                        {
                            Console.WriteLine("Город не найден!");
                        }

                        break;
                    }
                }
            }
        }
    }
}