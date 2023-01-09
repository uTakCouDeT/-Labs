using System.Net.Sockets;
using System.Text;
using System.Net;
using Microsoft.EntityFrameworkCore;
using lab_10;

var server = new TcpListener(IPAddress.Any, 8888);

try
{
    server.Start();
    Console.WriteLine("Сервер запущен. Ожидание подключений...");

    while (true)
    {
        using var tcpClient = await server.AcceptTcpClientAsync();
        Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");
        var stream = tcpClient.GetStream();
        int bytesRead = 10;
        while (true)
        {
            var data = new List<byte>();
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                data.Add(Convert.ToByte(bytesRead));
            }

            var word = Encoding.UTF8.GetString(data.ToArray());
            float? priceToday = null;
            if (word == "exit")
            {
                Console.WriteLine("Подключение закрыто");
                break;
            }

            Console.WriteLine($"Тикер - {word}");

            using (Lab10DbContext db = new Lab10DbContext())
            {
                var tickers = db.Tickers.ToList();
                var prices = (from price in db.Prices.Include(p => p.Ticker)
                    where price.Ticker.Ticker1 == word
                    orderby price
                    select price).ToList();
                if (prices.Count != 0)
                {
                    priceToday = (float)prices[0].Price1;
                }
            }

            if (priceToday != null)
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"{Convert.ToString(priceToday)}\n");
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            else
            {
                byte[] buffer = Encoding.UTF8.GetBytes("Тикер не найден\n");
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
catch (Exception exception)
{
    Console.WriteLine($"{exception.Message}");
}
finally
{
    server.Stop();
}