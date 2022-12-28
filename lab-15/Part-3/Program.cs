using System;
using System.Data.SqlTypes;

namespace sharp15
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleRandomizer s = SingleRandomizer.getInstance("первый");
            Console.WriteLine(s.SingRandomize());
            Console.WriteLine(s.check);

            (new Thread(() =>
            {
                SingleRandomizer s2 = SingleRandomizer.getInstance("четвёртый");
                Console.WriteLine(s2.SingRandomize());
                Console.WriteLine(s2.check);
            })).Start();

            SingleRandomizer s1 = SingleRandomizer.getInstance("второй");
            Console.WriteLine(s1.SingRandomize());
            Console.WriteLine(s1.check);

            SingleRandomizer s2 = SingleRandomizer.getInstance("третий");
            Console.WriteLine(s2.SingRandomize());
            Console.WriteLine(s2.check);
        }


        class SingleRandomizer
        {
            private static SingleRandomizer instance;
            private static object syncRoot = new Object();
            public string check;

            private SingleRandomizer(string name)
            {
                check = name;
            }

            public int SingRandomize()
            {
                Random rand = new Random();
                return rand.Next(0, 10000);
            }

            public static SingleRandomizer getInstance(string name)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new SingleRandomizer(name);
                    else
                    {
                        Console.WriteLine("Нельзя создать новый экземпляр, остаётся тот же");
                    }
                }

                return instance;
            }
        }
    }
}