using System.Reflection;

public class Program
{
    static void Main(string[] args)
    {
        Assembly asm = Assembly.LoadFrom("AnimalLib.dll");
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine(asm.FullName);
        Console.WriteLine("----------------------------------------------------------------");
        Type[] types = asm.GetTypes();
        foreach (Type t in types)
        {
            Console.WriteLine(t.Name);
        }

        /*
        var type = typeof(Animal);

        Console.WriteLine("Properties: ");
        foreach (var p in type.GetProperties())
        {
            Console.WriteLine(p.Name);
        }
        */
    }
}