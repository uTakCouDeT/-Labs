using System.Net;
using System.Net.Sockets;
using System.Text;

using TcpClient tcpClient = new TcpClient();
try
{
    await tcpClient.ConnectAsync("127.0.0.1", 8888);
    if (tcpClient.Connected)
    {
        Console.WriteLine($"Подключение с {tcpClient.Client.RemoteEndPoint} установлено");
    }
    else
    {
        Console.WriteLine("Не удалось подключиться");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// получаем поток для взаимодействия с сервером
NetworkStream stream = tcpClient.GetStream();

// буфер для входящих данных
var response = new List<byte>();
int bytesRead = 10; // для считывания байтов из потока
string line;

while (true)
{
    Console.WriteLine("Введите название тикера или \"exit\", чтобы закрыть подключение");
    line = Console.ReadLine();

    // считыванием строку в массив байт
    byte[] data = Encoding.UTF8.GetBytes(line + '\n');

    // отправляем данные
    await stream.WriteAsync(data);

    if (line == "exit")
    {
        break;
    }

    // считываем данные до конечного символа
    while ((bytesRead = stream.ReadByte()) != '\n')
    {
        // добавляем в буфер
        response.Add((byte)bytesRead);
    }

    var translation = Encoding.UTF8.GetString(response.ToArray());
    Console.WriteLine($"Тикер {line}: {translation}");

    response.Clear();
}

tcpClient.Close(); // закрываем подключение

if (tcpClient.Connected)
{
    Console.WriteLine("Не удалось закрыть подклчение");
}
else
{
    Console.WriteLine("Подключение закрыто");
}