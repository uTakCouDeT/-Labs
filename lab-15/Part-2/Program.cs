using System.Text.Json;

class Class1
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Тип файла: ");
            Console.WriteLine("1. - txt ");
            Console.WriteLine("2. - json");
            Console.WriteLine("0. - Выход");
            int type = int.Parse(Console.ReadLine());

            switch (type)
            {
                case 1:
                {
                    var txt = new MyLogger(new TxtRepository("../../../../Part-2/logger.txt"));
                    Console.WriteLine("1. - Записать в файл");
                    Console.WriteLine("2. - Удалить файл");
                    int mode = int.Parse(Console.ReadLine());
                    switch (mode)
                    {
                        case 1:
                        {
                            Console.WriteLine("Введите сообщение: ");
                            string line = Console.ReadLine();
                            txt.AddLogMessage(new LogMessage(line));
                            break;
                        }
                        case 2:
                            txt.DeleteFile();
                            break;
                    }

                    break;
                }
                case 2:
                {
                    var json = new MyLogger(new JsonRepository("../../../../Part-2/logger.json"));
                    Console.WriteLine("1. - Записать в файл");
                    Console.WriteLine("2. - Удалить файл");
                    int mode = int.Parse(Console.ReadLine());
                    switch (mode)
                    {
                        case 1:
                        {
                            Console.WriteLine("Введите сообщение: ");
                            string field = Console.ReadLine();
                            json.AddLogMessage(new LogMessage(field));
                            break;
                        }
                        case 2:
                            json.DeleteFile();
                            break;
                    }

                    break;
                }
            }

            if (type == 0)
            {
                break;
            }
        }
    }

    public class LogMessage
    {
        public string Message { get; set; }

        public LogMessage(string message)
        {
            Message = message;
        }

        public string GetWord()
        {
            return Message;
        }
    }

    internal interface IRepository
    {
        public void Create();
        public void Update(LogMessage obj);
        public void Delete();
    }

    internal class JsonRepository : IRepository
    {
        private string Path { get; set; }

        public JsonRepository(string filePath)
        {
            Path = filePath;
        }

        public void Create()
        {
            if (File.Exists(Path))
            {
                using var jsonFile = File.OpenWrite(Path);
            }
            else
            {
                using var jsonFile = File.CreateText(Path);
            }
        }

        public void Update(LogMessage unit)
        {
            using var jsonFile = new FileStream(Path, FileMode.OpenOrCreate);
            if (jsonFile.Length == 0)
            {
                List<LogMessage> dataBases = new List<LogMessage>();
                dataBases.Add(unit);
                JsonSerializer.Serialize(jsonFile, dataBases);
            }
            else
            {
                List<LogMessage> temp = JsonSerializer.Deserialize<List<LogMessage>>(jsonFile);
                temp.Add(unit);
                jsonFile.SetLength(0);
                JsonSerializer.Serialize(jsonFile, temp);
            }
        }

        public void Delete()
        {
            if (!File.Exists(Path))
            {
                Console.WriteLine("Файл не существует");
                return;
            }

            try
            {
                File.Delete(Path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при удалении: {e.Message}");
            }
        }
    }

    internal class TxtRepository : IRepository
    {
        private string Path { set; get; }

        public TxtRepository(string filePath)
        {
            Path = filePath;
        }

        public void Create()
        {
            if (File.Exists(Path))
            {
                using var txt = File.OpenWrite(Path);
            }
            else
            {
                using var txt = File.CreateText(Path);
            }

            ;
        }

        public void Update(LogMessage unit)
        {
            using var writer = new StreamWriter(Path, true);
            writer.WriteLine(unit.GetWord());
        }

        public void Delete()
        {
            if (!File.Exists(Path))
            {
                Console.WriteLine("Данного файла не существует");
                return;
            }

            try
            {
                File.Delete(Path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при удалении: {e.Message}");
            }
        }
    }

    internal class MyLogger
    {
        private readonly IRepository obj;

        public MyLogger(IRepository rep)
        {
            obj = rep;
            obj.Create();
        }

        public void AddLogMessage(LogMessage unit)
        {
            obj.Update(unit);
        }

        public void DeleteFile()
        {
            obj.Delete();
        }
    }
}