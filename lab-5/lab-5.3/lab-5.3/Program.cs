class MyDictionary<TKey, TValue>
{
    public TKey[] keys = new TKey[5];
    public TValue[] values = new TValue[5];
    int size = 5;
    int index = 0;

    //public List<TKey> keys = new List<TKey>();
    //public List<TValue> value = new List<TValue>();

    public MyDictionary(TKey[] _keys, TValue[] _values)
    {
        keys = new TKey[_keys.Count()];
        values = new TValue[_values.Count()];
        int i = 0;

        foreach (TKey el in _keys)
        {
            keys[i] = el;
            ++i;
        }

        i = 0;
        foreach (TValue val in _values)
        {
            values[i] = val;
            ++i;
        }

        size = _keys.Count();
    }

    public void Add(TKey[] _keys, TValue[] _values)
    {
        TKey[] tmpKeys = new TKey[_keys.Count() + size];
        TValue[] tmpValues = new TValue[_values.Count() + size];

        for (int i = 0; i < size; ++i)
        {
            tmpKeys[i] = keys[i];
            tmpValues[i] = values[i];
        }

        for (int k = size; k < _keys.Count() + size; ++k)
        {
            tmpKeys[k] = _keys[k - size];
            tmpValues[k] = _values[k - size];
        }

        keys = new TKey[_keys.Count() + size];
        values = new TValue[_values.Count() + size];

        keys = tmpKeys;
        values = tmpValues;
        size = size + _keys.Count();
    }

    public TValue this[TKey elem]
    {
        get
        {
            int elemIndex = -1;
            for (int i = 0; i < size; ++i)
            {
                if (keys[i].Equals(elem))
                {
                    elemIndex = i;
                    break;
                }
            }

            return values[elemIndex];
        }
        set
        {
            int elemIndex = -1;
            for (int i = 0; i < size; ++i)
            {
                if (keys[i].Equals(elem))
                {
                    elemIndex = i;
                    break;
                }
            }

            values[elemIndex] = value;
        }
    }

    public void Print()
    {
        for (int i = 0; i < size; ++i)
        {
            Console.Write(keys[i]);
            Console.Write(" : ");
            Console.Write(values[i]);
            Console.WriteLine();
        }
    }

    public int Size
    {
        get
        {
            Console.WriteLine("Размер: ");
            return size;
        }
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (int i = 0; i < size; ++i)
        {
            yield return values[i];
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[] arr1 = new int[] { 1, 44, 55, 99, 10 };
        string[] arr2 = new string[] { "art", "usd", "333", "marc", "c++" };

        MyDictionary<int, string> intString = new MyDictionary<int, string>(arr1, arr2);
        Console.WriteLine(intString.Size);
        intString.Print();
        Console.WriteLine("======================");

        int[] arr3 = new int[] { 6, 34, 88, 121, 46 };
        string[] arr4 = new string[] { "js", "go", "python", "java", "c#" };

        intString.Add(arr3, arr4);
        Console.WriteLine(intString.Size);
        intString.Print();
        Console.WriteLine("======================");

        foreach (string elem in intString)
        {
            Console.WriteLine(elem);
        }

        Console.WriteLine("======================");


        string[] stringKey = new string[] { "one", "two", "three", "four", "five" };
        double[] doubleVal = new double[] { 1.22, 9.323, 8.901, 99, 10.101010 };

        MyDictionary<string, double> stringDouble = new MyDictionary<string, double>(stringKey, doubleVal);
        foreach (double elem in stringDouble)
        {
            Console.WriteLine(elem);
        }

        Console.WriteLine("======================");

        Console.WriteLine(stringDouble["two"]);
    }
}