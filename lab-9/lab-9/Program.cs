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
        List<double> Avers = new List<double>();
        object locker = new object();
        using (StreamReader reader = new StreamReader(@"C:\Users\user\Documents\GitHub\csLabs\lab-9\lab-9\ticker.txt"))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                tickers.Add(line);
            }
        }

        int k = 0;
        foreach (var elem in tickers)
        {
            using (var client = new HttpClient())
            {
                string request =
                    $"https://query1.finance.yahoo.com/v7/finance/download/{elem}?period1={timeYearAgo}&period2={time}&interval=1d&events=history&includeAdjustedClose=true";

                try
                {
                    HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(request);
                    HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                    string content;
                    using (Stream stream = Response.GetResponseStream())
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
                        Console.WriteLine($"{++k}. {elem} - {average}");
                        Avers.Add(average);
                    });
                }
                catch (System.Net.WebException)
                {
                    Console.WriteLine($"{++k}. {elem} - None");
                }

                // try
                // {
                //     var content = await client.GetStringAsync(request);
                //     string[] text = content.Split('\n');
                //     var counter = Task.Factory.StartNew(() =>
                //     {
                //         double sum = 0;
                //         double average = 0;
                //         for (int i = 1; i < text.Length; ++i)
                //         {
                //             sum += (double.Parse(text[i].Split(',')[2]) + double.Parse(text[i].Split(',')[3])) / 2;
                //         }
                //
                //         average = sum / text.Length;
                //         Console.WriteLine($"{++k}. {elem} - {average}");
                //         Avers.Add(average);
                //     });
                // }
                // catch (System.Net.Http.HttpRequestException)
                // {
                //     Console.WriteLine($"{++k}. {elem} - None");
                // }
            }
        }

        using (StreamWriter write = new StreamWriter("tickersAnd.txt"))
        {
            for (int i = 0; i < Avers.Count; ++i)
            {
                lock (locker)
                {
                    write.WriteLine($"{tickers[i]}: {Avers[i]}");
                }
            }
        }
    }
}