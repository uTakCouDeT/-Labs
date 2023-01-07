using System.Net;
using System.Net.Sockets;
using System.Text;

using TcpClient tcpClient = new TcpClient();
try
{
    await tcpClient.ConnectAsync(IPAddress.Any, 8888);
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

Console.WriteLine("Введите название тикера: ");

// определяем отправляемые данные
var word = Console.ReadLine();

while (word != "exit")
{
    // считыванием строку в массив байт
    // при отправке добавляем маркер завершения сообщения - \n
    byte[] data = Encoding.UTF8.GetBytes(word + '\n');
    // отправляем данные
    await stream.WriteAsync(data);

    // считываем данные до конечного символа
    while ((bytesRead = stream.ReadByte()) != '\n')
    {
        // добавляем в буфер
        response.Add((byte)bytesRead);
    }

    var translation = Encoding.UTF8.GetString(response.ToArray());
    Console.WriteLine($"Слово {word}: {translation}");

    // выводим данные на консоль
    Console.WriteLine(response);

    response.Clear();

    Console.WriteLine("\nВведите название тикера или \"exit\", чтобы закрыть подключение");
    word = Console.ReadLine();
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