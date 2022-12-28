using System.Globalization;
using System.Net;

namespace lab_10;

internal static class Program
{
    static async Task Main()
    {
        using (Lab10DbContext db = new Lab10DbContext())
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            long nowTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            long yesterdayTime = nowTime - 4 * 86400;
            List<string> tickers = new List<string>();
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

                string request =
                    $"https://query1.finance.yahoo.com/v7/finance/download/{ticker}?period1={yesterdayTime}&period2={nowTime}&interval=1d&events=history&includeAdjustedClose=true";

                try
                {
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

                    Ticker dbTicker = new Ticker { ticker = $"{ticker}" };
                    db.Tickers.Add(dbTicker);

                    Console.WriteLine($"{index + 1}. {ticker}");
                    string[] text = content.Split('\n');
                    var counter = Task.Factory.StartNew(() =>
                    {
                        for (int i = 1; i < text.Length; ++i)
                        {
                            var average = (double.Parse(text[i].Split(',')[2]) +
                                           double.Parse(text[i].Split(',')[3])) / 2;
                            var date = text[i].Split(',')[0];
                            Price dbPrice = new Price { price = average, date = date, tickerid = dbTicker.id };
                            db.Prices.Add(dbPrice);
                        }

                        var pricesList = db.Prices.ToList();
                        var state = pricesList[pricesList.Count - 1].price - pricesList[pricesList.Count - 2].price;
                        TodaysCondition dbTodaysCondition = new TodaysCondition
                            { state = state, tickerid = dbTicker.id };
                        db.TodaysConditions.Add(dbTodaysCondition);
                    });
                }
                catch (WebException)
                {
                    Console.WriteLine($"{index + 1}. {ticker} - None");
                }
            }
            db.SaveChanges();
        }
    }
}