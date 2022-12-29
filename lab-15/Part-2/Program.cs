using System;
using System.Text.Json;
using System.Text.Json.Serialization;


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
                    var txt = new MyLogger(new TxtRep("../../../../Part-2/logger.txt"));
                    Console.WriteLine("1. - Записать в файл");
                    Console.WriteLine("2. - Удалить файл");
                    int mode = int.Parse(Console.ReadLine());
                    switch (mode)
                    {
                        case 1:
                        {
                            int index = int.Parse(Console.ReadLine());
                            string word = Console.ReadLine();
                            txt.LogMessage(new DataBase(index, word));
                            break;
                        }
                        case 2:
                            txt.DeleteRepo();
                            break;
                    }

                    break;
                }
                case 2:
                {
                    var json = new MyLogger(new JsonRep("../../../../Part-2/logger.json"));
                    Console.WriteLine("1. - Записать в файл");
                    Console.WriteLine("2. - Удалить файл");
                    int mode = int.Parse(Console.ReadLine());
                    switch (mode)
                    {
                        case 1:
                        {
                            int index = int.Parse(Console.ReadLine());
                            string word = Console.ReadLine();
                            json.LogMessage(new DataBase(index, word));
                            break;
                        }
                        case 2:
                            json.DeleteRepo();
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

    public class DataBase
    {
        public DataBase(int id, string word)
        {
            Id = id;
            Word = word;
        }

        public string GetWord()
        {
            return Word;
        }

        public int Id { get; set; }
        public string Word { get; set; }
    }

    internal interface MyRepository
    {
        public void CreateRepo();
        public void UpdateRepo(DataBase obj);
        public void DeleteRepo();
    }

    internal class JsonRep : MyRepository
    {
        private string Path { get; set; }

        public JsonRep(string filePath)
        {
            Path = filePath;
        }

        public void CreateRepo()
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

        public void UpdateRepo(DataBase unit)
        {
            using var jsonFile = new FileStream(Path, FileMode.OpenOrCreate);
            if (jsonFile.Length == 0)
            {
                List<DataBase> dataBases = new List<DataBase>();
                dataBases.Add(unit);
                JsonSerializer.Serialize(jsonFile, dataBases);
            }
            else
            {
                List<DataBase> temp = JsonSerializer.Deserialize<List<DataBase>>(jsonFile);
                temp.Add(unit);
                jsonFile.SetLength(0);
                JsonSerializer.Serialize(jsonFile, temp);
            }
        }

        public void DeleteRepo()
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

    internal class TxtRep : MyRepository
    {
        private string Path { set; get; }

        public TxtRep(string filePath)
        {
            Path = filePath;
        }

        public void CreateRepo()
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

        public void UpdateRepo(DataBase unit)
        {
            using var writer = new StreamWriter(Path, true);
            writer.WriteLine(unit.GetWord());
        }

        public void DeleteRepo()
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

    internal class MyLogger
    {
        private readonly MyRepository repositoryObject;

        public MyLogger(MyRepository rep)
        {
            repositoryObject = rep;
            repositoryObject.CreateRepo();
        }

        public void LogMessage(DataBase unit)
        {
            repositoryObject.UpdateRepo(unit);
        }

        public void DeleteRepo()
        {
            repositoryObject.DeleteRepo();
        }
    }
}