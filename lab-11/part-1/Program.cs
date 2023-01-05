using System.Net;
using Microsoft.EntityFrameworkCore;
using lab_10;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Записать данные в бд? (y/n)");
        var mode = Console.ReadLine();
        if (mode == "y" || mode == "")
        {
            CreateDb();
            CreateConditions();
        }

        ChekerCondition();
    }

    static void CreateConditions()
    {
        using (Lab10DbContext db = new Lab10DbContext())
        {
            var tickers = db.Tickers.ToList();
            int IdConditions = 1;
            foreach (Ticker ticker in tickers)
            {
                var prices = (from price in db.Prices.Include(p => p.Ticker)
                    where price.Ticker == ticker
                    select price).ToList();
                var priceConditions = new TodaysCondition()
                {
                    Id = IdConditions++,
                    TickerId = ticker.Id,
                    Ticker = ticker,
                    State = prices[0].Price1 - prices[1].Price1
                };

                db.TodaysConditions.Add(priceConditions);
            }

            db.SaveChanges();
        }
    }

    static void ChekerCondition()
    {
        Console.WriteLine("Введите название тикера: ");
        string tickerName = Console.ReadLine();
        while (tickerName != "exit")
        {
            using (Lab10DbContext db = new Lab10DbContext())
            {
                var prices = (from price in db.Prices.Include(p => p.Ticker)
                    where price.Ticker.Ticker1 == tickerName
                    select price).ToList();
                var condition = (from con in db.TodaysConditions.Include(p => p.Ticker)
                    where con.Ticker.Ticker1 == tickerName
                    select con).ToList();
                foreach (var price in prices)
                    Console.WriteLine($"[{price.Date}] {price.Ticker.Ticker1} - {price.Price1}");
                foreach (var con in condition)
                    Console.WriteLine($"State: {con.State}");
            }

            Console.WriteLine("\nВведите название тикера или \"exit\", чтобы выйти");
            tickerName = Console.ReadLine();
        }
    }

    static async void CreateDb()
    {
        string path = "../../../../part-1/ticker.txt";
        int IdTicker = 1;
        int IdPrice = 1;
        using (Lab10DbContext db = new Lab10DbContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            using (StreamReader TickersReader = File.OpenText(path))
            {
                while (true)
                {
                    string line = TickersReader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    var nowTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                    var twoWeeksAgoTime = nowTime - 2 * 86400;
                    try
                    {
                        var request =
                            $"https://query1.finance.yahoo.com/v7/finance/download/{line}?period1={twoWeeksAgoTime}&period2={nowTime}&interval=1d&events=history&includeAdjustedClose=true";
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
                        List<double> listAvNum = new List<double>();
                        double summa = 0;
                        for (int i = 1; i < lines.Length; ++i)
                        {
                            listAvNum.Add(GetAverageNum(lines[i]));
                        }

                        var newTicker = new Ticker()
                        {
                            Id = IdTicker,
                            Ticker1 = line
                        };
                        ++IdTicker;
                        var priceForTicker1 = new Price()
                        {
                            Id = IdPrice,
                            Ticker = newTicker,
                            TickerId = newTicker.Id,
                            Price1 = GetAverageNum(lines[1]),
                            Date = DateTime.Now
                        };
                        ++IdPrice;
                        var priceForTicker2 = new Price()
                        {
                            Id = IdPrice,
                            Ticker = newTicker,
                            TickerId = newTicker.Id,
                            Price1 = GetAverageNum(lines[2]),
                            Date = DateTime.Now.AddDays(-1)
                        };
                        ++IdPrice;

                        Console.WriteLine($"{line} - Записан");
                        db.Prices.Add(priceForTicker1);
                        db.Prices.Add(priceForTicker2);
                        db.Tickers.Add(newTicker);
                    }
                    catch (Exception ex)
                    {
                        Console.Write($"{line} - Данные не получены: ");
                        Console.WriteLine(ex.Message);
                    }
                }

                db.SaveChanges();
            }
        }
    }

    static double GetAverageNum(string data)
    {
        double High = 0;
        double Low = 0;

        string[] fields = data.Split(',');
        if (fields[2] != "null")
        {
            High = Double.Parse(fields[2].Replace('.', ','));
        }


        if (fields[3] != "null")
        {
            Low = Double.Parse(fields[3].Replace('.', ','));
        }

        return (High + Low) / 2;
    }
}