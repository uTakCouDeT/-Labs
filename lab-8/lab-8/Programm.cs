using System.Reflection;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO.Compression;
using AnimalLib;

public class Program
{
    static void Main(string[] args)
    {
        Animal animal = new Animal("Russia", true, "Ivan", "Bear");

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Animal));
        string path = "C:\\Users\\user\\Documents\\GitHub\\csLabs\\lab-8\\lab-8\\Result.xml";

        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            xmlSerializer.Serialize(fs, animal);
            Console.WriteLine($"File saved as {path}");
        }

        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            Console.WriteLine("Deserialization:");
            Animal? newAnimal = xmlSerializer.Deserialize(fs) as Animal;
            Console.WriteLine($" - Country: {newAnimal?.Country} " +
                              $"\n - HideFromOtherAnimals: {newAnimal?.HideFromOtherAnimals} " +
                              $"\n - Name: {newAnimal?.Name}  " +
                              $"\n - WhatAnimal: {newAnimal?.WhatAnimal}");
        }

        // Задание 2

        Console.WriteLine("Введите директорию для поиска:");
        //string catalog = Console.ReadLine();
        Console.WriteLine("Введите имя файла:");
        //string fileName = Console.ReadLine();

        string catalog = @"C:\Users\user\Documents\GitHub\csLabs\lab-8";
        string fileName = "Result.xml";

        foreach (string file in Directory.EnumerateFiles(catalog, fileName, SearchOption.AllDirectories))
        {
            FileInfo info = new FileInfo(file);
            Console.WriteLine($"{info.Name}\n - Путь: {info.FullName}\n - Размер: {info.Length} байт");

            // Тут будет вывод файла

            Console.WriteLine("Архивировать данный файл? (y/n)");
            if (Console.ReadLine() == "y")
            {
                string sourceFile = info.FullName;
                string compressedFile = info.FullName.Substring(0, info.FullName.IndexOf(info.Name)) +
                                        info.Name.Substring(0, info.Name.IndexOf(".")) + ".gz";
                string targetFile = info.FullName.Substring(0, info.FullName.IndexOf(info.Name)) + "aboba.xml";
                Console.WriteLine(targetFile);

                // создание сжатого файла
                CompressAsync(sourceFile, compressedFile);

                async Task CompressAsync(string sourceFile, string compressedFile)
                {
                    // поток для чтения исходного файла
                    using FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate);
                    
                    // поток для записи сжатого файла
                    using FileStream targetStream = File.Create(compressedFile);

                    // поток архивации
                    using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
                    await sourceStream.CopyToAsync(compressionStream); // копируем байты из одного потока в другой

                    Console.WriteLine($"Сжатие файла {sourceFile} завершено.");
                    Console.WriteLine($"Исходный размер: {sourceStream.Length}  сжатый размер: {targetStream.Length}");
                }
            }
        }
    }
}