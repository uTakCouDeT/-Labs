class Program
{
    static void Main(string[] args)
    {
        List<string> PathList = new List<string>();
        Console.WriteLine("Введите путь до директории: ");
        string path = Console.ReadLine();
        while (true)
        {
            WatchingDirectory watchingDirectory = new WatchingDirectory();
            Watcher watcher = new Watcher(path, watchingDirectory);
            while (true)
            {
                Console.Clear();
                watchingDirectory.NotifyObservers();
                Thread.Sleep(5000);
            }
        }
    }

    interface IObserver
    {
        void Update();
    }

    class Watcher : IObserver
    {
        public string Path { get; set; }
        private IObservable watchinDirectory;

        public Watcher(string dirPath, IObservable obs)
        {
            Path = dirPath;
            watchinDirectory = obs;
            watchinDirectory.RegisterObserver(this);
        }

        public void Update()
        {
            if (Directory.Exists(Path))
            {
                Console.WriteLine($"Содержимое директории {Path} ");

                Console.WriteLine("Директории: ");
                string[] dirs = Directory.GetDirectories(Path);
                foreach (string elem in dirs)
                {
                    Console.WriteLine(elem);
                }

                Console.WriteLine();

                Console.WriteLine("Файлы: ");
                string[] files = Directory.GetFiles(Path);
                foreach (string elem in files)
                {
                    Console.WriteLine(elem);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Данной директории не существует");
            }
        }

        public void StopWatching()
        {
            watchinDirectory.RemoveObserver(this);
            watchinDirectory = null;
        }
    }

    interface IObservable
    {
        void RegisterObserver(IObserver obj);
        void RemoveObserver(IObserver obj);
        void NotifyObservers();
    }


    class WatchingDirectory : IObservable
    {
        List<IObserver> observers;

        public WatchingDirectory()
        {
            observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver unit)
        {
            observers.Add(unit);
        }

        public void RemoveObserver(IObserver unit)
        {
            observers.Remove(unit);
        }

        public void NotifyObservers()
        {
            Console.Clear();
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
        }
    }
}