using System;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace lab_2._3
{
    
    
    class DocumentWorker
    {
        public virtual void OpenDocument()
        {
            Console.WriteLine("Документ открыт");
        }

        public virtual void EditDocument()
        {
            Console.WriteLine("Редактирование документа доступно в версии Pro");
        }

        public virtual void SaveDocument()
        {
            Console.WriteLine("Сохранение документа доступно в версии Pro");
        }
        
    }

    class ProDocumentWorker : DocumentWorker
    {
        public override void EditDocument()
        {
            Console.WriteLine("Документ отредактирован");
        }

        public override void SaveDocument()
        {
            Console.WriteLine("Документ сохранен в старом формате, сохранение в остальных форматах доступно в версии Expert");
        }
    }
    
    class ExpertDocumentWorker : ProDocumentWorker
    {
        public override void SaveDocument()
        {
            Console.WriteLine("Документ сохранен в новом формате");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Ввети ключ доступа [press 1]");
            Console.WriteLine("Пользоваться бесплатной версией [press any key]");

            var f = Convert.ToInt32(Console.ReadLine());
            var worker = new DocumentWorker();
            if (f == 1){
                    Console.WriteLine("Введите ключ доступа");
                    string key = Console.ReadLine();
                    switch (key)
                    {
                        case "ProKey":
                            Console.WriteLine("Pro версия активирована!");
                            worker = new ProDocumentWorker();
                            break;
                        case "ExpertKey":
                            Console.WriteLine("Expert версия активирована!");
                            worker = new ExpertDocumentWorker();
                            break;
                        default:
                            Console.WriteLine("Ключ введён не корректно");
                            break;
                    }
            }
            Console.WriteLine("\n Проверка методов:");
            worker.EditDocument();
            worker.OpenDocument();
            worker.SaveDocument();
        }
    }
}