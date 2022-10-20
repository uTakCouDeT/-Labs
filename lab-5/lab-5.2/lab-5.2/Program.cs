class MyList<T>
{
    public T[] newList = new T[5];
    int size = 5;
    int index = 0;

    public MyList(params T[] tmpList)
    {
        newList = new T[tmpList.Count()];
        size = tmpList.Count();
        foreach (T elem in tmpList)
        {
            newList[index] = elem;
            index++;
        }
    }

    public void Add(params T[] tmpList)
    {
        if (tmpList.Count() >= size - newList.Count())
        {
            T[] temp = new T[newList.Count()];
            for (int i = 0; i < index; i++)
            {
                temp[i] = newList[i];
            }

            newList = new T[tmpList.Count() + size];
            size = tmpList.Count() + size;
            for (int i = 0; i < index; i++)
            {
                newList[i] = temp[i];
            }
        }

        foreach (T elem in tmpList)
        {
            newList[index] = elem;
            index++;
        }
    }

    public T this[int elem]
    {
        get { return newList[elem]; }
        set { newList[elem] = value; }
    }

    public int Size
    {
        get
        {
            Console.Write("MyList size: ");
            return newList.Count();
        }
    }

    public void Print()
    {
        for (int i = 0; i < index; i++)
        {
            Console.Write(newList[i]);
            Console.Write(" ");
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyList<int> loo = new MyList<int>(5, 6, 3, 5, 5, 12, 7, 87);
        loo.Print();
        Console.WriteLine(loo.Size);

        loo.Add(5, 4, 1, 2, 6, 7, 5, 6, 9, 10, 199, 99454);
        loo.Print();
        Console.WriteLine(loo.Size);

        loo.Add(1337);
        loo.Print();
        Console.WriteLine(loo.Size);

        Console.WriteLine(loo[20]);
    }
}