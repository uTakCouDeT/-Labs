using System.Reflection;
using System.Xml;

public class Program
{
    static void Main(string[] args)
    {
        Assembly asm = Assembly.LoadFrom("AnimalLib.dll");
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine(asm.FullName);
        Console.WriteLine("----------------------------------------------------------------");
        Type[] types = asm.GetTypes();
        for (int t = 3; t < types.Length; t++)
        {
            if (types[t].IsAbstract)
            {
                Console.WriteLine("\nAbstract class:");
            }
            else if (types[t].IsClass)
            {
                Console.WriteLine("\nClass:");
            }
            else if (types[t].IsEnum)
            {
                Console.WriteLine("\nEnum:");
            }

            Console.WriteLine($" - {types[t].Name}");
            Console.WriteLine("Properties: ");
            foreach (var prop in types[t].GetProperties())
            {
                Console.WriteLine($" - {prop.Name}");
            }

            Console.WriteLine("Methods: ");
            var methods = types[t].GetMethods();
            for (int m = 0; m < methods.Length; m++)
            {
                Console.WriteLine($" - {methods[m].Name}");
            }
        }

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Result.xml");
        XmlElement? xRoot = xDoc.DocumentElement;

// создаем новый элемент person
        XmlElement personElem = xDoc.CreateElement("person");

// создаем атрибут name
        XmlAttribute nameAttr = xDoc.CreateAttribute("name");

// создаем элементы company и age
        XmlElement companyElem = xDoc.CreateElement("company");
        XmlElement ageElem = xDoc.CreateElement("age");

// создаем текстовые значения для элементов и атрибута
        XmlText nameText = xDoc.CreateTextNode("Mark");
        XmlText companyText = xDoc.CreateTextNode("Facebook");
        XmlText ageText = xDoc.CreateTextNode("30");

//добавляем узлы
        nameAttr.AppendChild(nameText);
        companyElem.AppendChild(companyText);
        ageElem.AppendChild(ageText);

// добавляем атрибут name
        personElem.Attributes.Append(nameAttr);
// добавляем элементы company и age
        personElem.AppendChild(companyElem);
        personElem.AppendChild(ageElem);
// добавляем в корневой элемент новый элемент person
        xRoot?.AppendChild(personElem);
// сохраняем изменения xml-документа в файл
        xDoc.Save("Result.xml");
    }
}