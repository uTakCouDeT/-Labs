using System.Globalization;
using System.Net;

namespace lab_10;

internal static class Program
{
    static async Task Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        int IdTicker = 1;
        int IdPrice = 1;
        using (Lab10DbContext db = new Lab10DbContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            using (StreamReader TickerReader = File.OpenText("../../../../lab-10/ticker.txt"))
            {
                while (TickerReader.ReadLine() != null)
                {
                    string ticker = TickerReader.ReadLine();
                    try
                    {
                        var nowTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                        var twoWeeksAgoTime = nowTime - 365 * 86400;

                        var request =
                            $"https://query1.finance.yahoo.com/v7/finance/download/{ticker}?period1={twoWeeksAgoTime}&period2={nowTime}&interval=1d&events=history&includeAdjustedClose=true";
                        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(request);
                        HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                        string content;
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                content = reader.ReadToEnd();
                            }
                        }

                        string[] lines = content.Split('\n');
                        Console.WriteLine($"----------- {ticker} -----------");

                        foreach (var VARIABLE in lines)
                        {
                            Console.WriteLine(VARIABLE);
                        }

                        var newTicker = new Ticker()
                        {
                            id = IdTicker,
                            ticker = ticker
                        };
                        ++IdTicker;

                        var splitLastLine = lines[lines.Length - 1].Split(',');
                        var highLastLineString = splitLastLine[2];
                        double? highLastLine = null;
                        if (highLastLineString != "null")
                        {
                            highLastLine = double.Parse(highLastLineString);
                        }

                        var lowLastLineString = splitLastLine[3];
                        double? lowLastLine = null;
                        if (lowLastLineString != "null")
                        {
                            lowLastLine = double.Parse(lowLastLineString);
                        }

                        Console.WriteLine(lines[0]);
                        Console.WriteLine(lines[lines.Length - 1]);

                        var priceForTicker1 = new Price()
                        {
                            id = IdPrice,
                            Ticker = newTicker,
                            tickerid = newTicker.id,
                            price = (highLastLine + lowLastLine) / 2,
                            date = splitLastLine[0]
                        };
                        ++IdPrice;

                        var splitPreLastLine = lines[lines.Length - 2].Split(',');
                        var highPreLastLineString = splitPreLastLine[2];
                        double? highPreLastLine = null;
                        if (highPreLastLineString != "null" && splitPreLastLine[0] != "Date")
                        {
                            highPreLastLine = double.Parse(highPreLastLineString);
                        }

                        var lowPreLastLineString = splitPreLastLine[3];
                        double? lowPreLastLine = null;
                        if (lowPreLastLineString != "null" && splitPreLastLine[0] != "Date")
                        {
                            lowPreLastLine = double.Parse(lowPreLastLineString);
                        }

                        Console.WriteLine(lines[0]);
                        Console.WriteLine(lines[lines.Length - 2]);
                        var priceForTicker2 = new Price()
                        {
                            id = IdPrice,
                            Ticker = newTicker,
                            tickerid = newTicker.id,
                            price = (highPreLastLine + lowPreLastLine) / 2,
                            date = splitPreLastLine[0]
                        };
                        ++IdPrice;

                        Console.WriteLine($"Check {ticker}");
                        db.Prices.Add(priceForTicker1);
                        db.Prices.Add(priceForTicker2);
                        db.Tickers.Add(newTicker);
                    }
                    catch (WebException)
                    {
                        Console.WriteLine("None");
                    }
                }

                db.SaveChanges();
            }
        }
    }
}