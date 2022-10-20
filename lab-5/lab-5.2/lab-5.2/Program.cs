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
            Console.Write("size: ");
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
        MyList<int> list = new MyList<int>(1, 2, 3, 5, 7, 12, 7, 111);
        list.Print();
        Console.WriteLine(list.Size);

        list.Add(5, 4, 3, 2, 6, 7, 4, 5, 9, 10, 34, 12);
        list.Print();
        Console.WriteLine(list.Size);

        list.Add(12345);
        list.Print();
        Console.WriteLine(list.Size);

        Console.WriteLine(list[20]);
    }
}