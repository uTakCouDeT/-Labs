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
            Console.WriteLine(type.Name);
            
            if ((type.Name != "CommentAttribute") && type.IsClass)
            {
                XElement element = new XElement($"{type.Name}");

                if (type.IsAbstract)
                {
                    XElement abstrClass = new XElement($"Abstract", " Abstract class ");
                    element.Add(abstrClass);
                }
                
                string commentsNames = "";
                foreach (Attribute comment in type.GetTypeInfo().GetCustomAttributes())
                {
                    if (comment is CommentAttribute animalComment)
                    {
                        commentsNames = animalComment.Comment;
                    }
                }
                XAttribute comments = new XAttribute("Attribute", commentsNames);

                string propsNames = " ";
                foreach (var property in type.GetProperties())
                {
                    propsNames += $"{property.Name} ";
                }

                XElement properties = new XElement("Proporties", propsNames);
                
                string methodsNames = " ";
                foreach (var method in type.GetMethods())
                {
                    if (method.Name.Substring(0, 4) != "get_" && method.Name.Substring(0, 4) != "set_" &&
                        method.Name != "Equals" && method.Name != "GetHashCode" && method.Name != "GetType" &&
                        method.Name != "ToString")
                    {
                        methodsNames += $"{method.Name} ";
                    }
                }
                XElement methods = new XElement("Methods", methodsNames);


                element.Add(comments);
                element.Add(properties);
                element.Add(methods);
                clss.Add(element);
            }

            if (type.IsEnum)
            {
                XElement element = new XElement($"{type.Name}");
                string enums = " ";
                foreach (var enumElem in type.GetEnumNames())
                {
                    enums += $"{enumElem} ";
                }
                
                element.Add(enums);
                othr.Add(element);
            }
        }

        othr.Add(clss);
        xDoc.Add(othr);
        xDoc.Save("C:\\Users\\user\\Documents\\GitHub\\csLabs\\lab-7\\lab-7\\Result.xml");
    }
}