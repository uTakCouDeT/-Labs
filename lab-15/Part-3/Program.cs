class Program
{
    static void Main(string[] args)
    {
        SingleRandomizer randomizerA = SingleRandomizer.getInstance("Randomizer - A");
        Console.WriteLine(randomizerA.Next());
        Console.WriteLine(randomizerA.name);

        SingleRandomizer randomizerB = SingleRandomizer.getInstance("Randomizer - B");
        Console.WriteLine(randomizerB.Next());
        Console.WriteLine(randomizerB.name);

        (new Thread(() =>
        {
            SingleRandomizer randomizerC = SingleRandomizer.getInstance("Randomizer - C1");
            Console.WriteLine(randomizerC.Next());
            Console.WriteLine(randomizerC.name);
        })).Start();

        SingleRandomizer randomizerC = SingleRandomizer.getInstance("Randomizer - C2");
        Console.WriteLine(randomizerC.Next());
        Console.WriteLine(randomizerC.name);
    }


    class SingleRandomizer
    {
        private static SingleRandomizer instance;
        private Random rand;
        public string name { get; set; }

        private static object syncRoot = new Object();

        protected SingleRandomizer(string name)
        {
            rand = new Random();
            this.name = name;
        }

        public int Next()
        {
            return rand.Next(0, 10000);
        }

        public static SingleRandomizer getInstance(string name)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new SingleRandomizer(name);
                }
            }

            return instance;
        }
    }
}