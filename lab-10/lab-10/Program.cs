using System.Globalization;
using System.Net;
using Microsoft.EntityFrameworkCore;


class Program
{
    internal class Ticker
    {
        public int id { get; set; }
        public string ticker { get; set; }

        public Ticker()
        {
        }

        public Ticker(string ticker)
        {
            this.ticker = ticker;
        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Ticker> Tickers => Set<Ticker>();
        public ApplicationContext() => Database.EnsureCreated();
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../../../../lab-10/lab-10-db.sqlite");
        }
    }

    internal class Price
    {
        public int id { get; set; }
        public int tickerid { get; set; }
        public int price { get; set; }
        public int date { get; set; }
    }

    internal class TodaysCondition
    {
        public int id { get; set; }
        public int tickerid { get; set; }
        public int state { get; set; }
    }


    static async Task Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            // создаем два объекта User
            Ticker tom = new Ticker { ticker = "Tom"};
            Ticker alice = new Ticker { ticker = "Alice"};
 
            // добавляем их в бд
            db.Tickers.Add(tom);
            db.Tickers.Add(alice);
            db.SaveChanges();
            Console.WriteLine("Объекты успешно сохранены");
 
            // получаем объекты из бд и выводим на консоль
            var users = db.Tickers.ToList();
            Console.WriteLine("Список объектов:");
            foreach (Ticker u in users)
            {
                Console.WriteLine($"{u.id}. - {u.ticker}");
            }
        }
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

        using (StreamWriter write = new StreamWriter(@"../../../../lab-10/avg-ticker.txt"))
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