using System.Globalization;
using System.Linq;
using System.Net;

public class Weather
{
    public string Country { set; get; }
    public string Name { set; get; }
    private double Temp { set; get; }
    private string Description { set; get; }

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

    public void Print()
    {
        if (Country != "")
        {
            Console.Write($"{Country}, {Name}: ");
        }

        Console.WriteLine($"{Temp}°C ({Description})");
    }
}

class Program
{
    static bool NameLongerThanFour(string name)
    {
        return name.Length > 4;
    }
    static void Main(string[] args)
    {
        const string apiKey = "94b3e2fa33ad3e53428fa6b749f38213";
        Random rnd = new Random();
        List<Weather> weathers = new List<Weather>();
        for (int i = 0; i < 50; i++)
        {
            Weather weather = new Weather();
            while (weather.Country == "")
            {
                double lat = rnd.NextDouble() * 180 - 90;
                double lon = rnd.NextDouble() * 360 - 180;

                string apiRequest =
                    $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiRequest);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string txtJson;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        txtJson = reader.ReadToEnd();
                    }
                }

                weather = new Weather(txtJson);
            }
            weathers.Add(weather);
            Console.Clear();
            Console.WriteLine($"\n                     Loading {2 * (i + 1)}%");
            Console.WriteLine("||" + new string('=', i) + new string('-', 50 - i) + "||");
        }

        var query = weathers.Where(new Func<Weather, bool>());
        
        Console.Clear();
        for (int i = 0; i < 50; i++)
        {
            weathers[i].Print();
        }
    }
}