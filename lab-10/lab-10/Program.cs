using System.Globalization;
using System.Net;
using lab_10;
using Microsoft.EntityFrameworkCore;


class Program
{
    static async Task Main(string[] args)
    {
        using (Lab10DbContext db = new Lab10DbContext())
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            long time = DateTimeOffset.Now.ToUnixTimeSeconds();
            long timeYearAgo = time - 31556926;
            List<string> tickers = new List<string>();
            List<double> avg = new List<double>();
            object locker = new object();
            using (StreamReader reader = new StreamReader(@"../../../../lab-10/ticker.txt"))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    tickers.Add(line);
                }
            }

            for (var index = 0; index < tickers.Count; index++)
            {
                var ticker = tickers[index];
                using (var client = new HttpClient())
                {
                    string request =
                        $"https://query1.finance.yahoo.com/v7/finance/download/{ticker}?period1={timeYearAgo}&period2={time}&interval=1d&events=history&includeAdjustedClose=true";

                    try
                    {
                        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(request);
                        HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                        string content;
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                content = reader.ReadToEnd();
                            }
                        }

                        Console.WriteLine($"{index}. {ticker}: ");
                        string[] text = content.Split('\n');
                        var counter = Task.Factory.StartNew(() =>
                        {
                            for (int i = 1; i < text.Length; ++i)
                            {
                                var average = ((double.Parse(text[i].Split(',')[2]) +
                                                double.Parse(text[i].Split(',')[3])) / 2);
                                var date = text[i].Split(',')[0];
                                Ticker dbTicker = new Ticker { ticker = $"{ticker}" };
                                // Price dbPrice = new Price { price = average, date = date };
                                // var state = Double.Parse();
                                // TodaysCondition dbTodaysCondition = new TodaysCondition { state = state };
                                db.Tickers.Add(dbTicker);
                                db.SaveChanges();
                            }
                        });
                    }
                    catch (WebException)
                    {
                        Console.WriteLine($"{index}. {ticker} - None");
                    }
                }
            }
        }
    }
}