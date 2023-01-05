using System.Net.Sockets;
using System.Text;
using System.Net;
using Microsoft.EntityFrameworkCore;
using lab_10;

var server = new TcpListener(IPAddress.Any, 8888);

try
{
    server.Start();
    Console.WriteLine("server is ready...");

    while (true)
    {
        using var tcpClient = await server.AcceptTcpClientAsync();
        var stream = tcpClient.GetStream();
        int bytesRead = 0;
        while (true)
        {
            var data = new List<byte>();
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                data.Add(Convert.ToByte(bytesRead));
            }

            var word = Encoding.UTF8.GetString(data.ToArray());
            float priceToday = 0;
            if (word == "exit")
            {
                break;
            }

            Console.WriteLine($"Ticker is {word}");

            using (Lab10DbContext db = new Lab10DbContext())
            {
                var tickers = db.Tickers.ToList();
                var prices = (from price in db.Prices.Include(p => p.Ticker)
                    where price.Ticker.Ticker1 == word
                    orderby price
                    select price).ToList();

                priceToday = (float)prices[0].Price1;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(Convert.ToString(priceToday) + '\n');
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
finally
{
    server.Stop();
}