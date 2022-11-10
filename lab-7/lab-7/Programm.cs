using System.Reflection;
//using System.Xml;
using System.Xml.Linq;
using AnimalLib;

public class Program
{
    static void Main(string[] args)
    {
        Assembly asm = Assembly.LoadFrom("AnimalLib.dll");
        Console.WriteLine(new string('-', 64) + $"\n{asm.FullName}\n" + new string('-', 64));
        var types = asm.ExportedTypes;
        XDocument xDoc = new XDocument();
        XElement othr = new XElement(asm.GetName().Name);
        XElement clss = new XElement("Classes");

        foreach (var type in asm.ExportedTypes)
        {
            if (type.IsAbstract)
            {
                Console.WriteLine("\nAbstract class:");
            }
            else if (type.IsClass)
            {
                Console.WriteLine("\nClass:");
            }
            else if (type.IsEnum)
            {
                Console.WriteLine("\nEnum:");
            }

            Console.WriteLine($" - {type.Name}");
            Console.WriteLine("Properties: ");
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($" - {prop.Name}");
            }

            Console.WriteLine("Methods: ");
            var methods = type.GetMethods();
            foreach (var methodInfo in type.GetMethods())
            {
                Console.WriteLine($" - {methodInfo.Name}");
            }
        }
        
        foreach (var type in asm.ExportedTypes)
        {
            var elemType = asm.GetType($"AnimalLib.{type.Name}");

            if (type.IsAbstract)
            {
                XElement abstrClass = new XElement($"{type.Name}");
                clss.Add(abstrClass);
            }

            if ((type.Name != "CommentAttribute") && type.IsClass && !type.IsAbstract)
            {
                XElement tempElement = new XElement($"{type.Name}");
                var objectType = elemType.GetTypeInfo();


                string commentsNames = "";
                foreach (Attribute comment in objectType.GetCustomAttributes())
                {
                    if (comment is CommentAttribute animalComment)
                    {
                        commentsNames = animalComment.Comment;
                    }
                }

                XAttribute comments = new XAttribute("Attribute", commentsNames);
                //XElement comments = new XElement("Attribute", commentsNames);

                string fieldsNames = " ";
                foreach (var classField in type.GetFields())
                {
                    fieldsNames += $"{classField.Name} ";
                }

                XElement fields = new XElement("Field", fieldsNames);

                string propsNames = " ";
                foreach (var property in type.GetProperties())
                {
                    propsNames += $"{property.Name} ";
                }

                XElement properties = new XElement("Proporties", propsNames);

                string mathodsNames = " ";
                foreach (var method in type.GetMethods())
                {
                    if (method.Name.Substring(0, 3) != "get" && method.Name.Substring(0, 3) != "set")
                    {
                        if (method.Name != "Equals" && method.Name != "GetHashCode" && method.Name != "GetType" &&
                            method.Name != "ToString")
                        {
                            mathodsNames += $"{method.Name} ";
                        }
                        //mathodsNames += $"{method.Name} ";
                    }
                }

                XElement methods = new XElement("Methods", mathodsNames);


                tempElement.Add(comments);
                tempElement.Add(fields);
                tempElement.Add(properties);
                tempElement.Add(methods);
                clss.Add(tempElement);
            }

            if (type.IsEnum)
            {
                XElement allEnums = new XElement($"{type.Name}");
                string enums = " ";
                foreach (var enumElem in type.GetEnumNames())
                {
                    enums += $"{enumElem} ";
                }

                allEnums.Add(enums);
                othr.Add(allEnums);
            }
        }

        othr.Add(clss);
        xDoc.Add(othr);
        xDoc.Save("C:\\Users\\user\\Documents\\GitHub\\csLabs\\lab-7\\lab-7\\Result.xml");
    }
}