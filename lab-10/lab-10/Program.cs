using System.Globalization;
using System.Net;

class Program
{
    static async Task Main(string[] args)
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        long time = DateTimeOffset.Now.ToUnixTimeSeconds();
        long timeYearAgo = time - 31556926;
        List<string> tickers = new List<string>();
        List<double> avg = new List<double>();
        object locker = new object();
        using (StreamReader reader = new StreamReader(@"C:\Users\user\Documents\GitHub\csLabs\lab-10\lab-10\ticker.txt"))
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

                    string[] text = content.Split('\n');
                    var counter = Task.Factory.StartNew(() =>
                    {
                        double sum = 0;
                        double average = 0;
                        for (int i = 1; i < text.Length; ++i)
                        {
                            sum += (double.Parse(text[i].Split(',')[2]) + double.Parse(text[i].Split(',')[3])) / 2;
                        }

                        average = sum / text.Length;
                        Console.WriteLine($"{index}. {ticker} - {average}");
                        avg.Add(average);
                    });
                }
                catch (WebException)
                {
                    Console.WriteLine($"{index}. {ticker} - None");
                }
            }
        }

        using (StreamWriter write = new StreamWriter(@"C:\Users\user\Documents\GitHub\csLabs\lab-10\lab-10\avg-ticker.txt"))
        {
            for (int i = 0; i < avg.Count; ++i)
            {
                lock (locker)
                {
                    write.WriteLine($"{i}. {tickers[i]} - {avg[i]}");
                }
            }
        }
    }
}