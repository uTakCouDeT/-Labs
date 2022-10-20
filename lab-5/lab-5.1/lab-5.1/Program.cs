public class MyMatrix
{
    public double[,] matrix;
    public int lines;
    public int columns;

    public MyMatrix(int lineCount, int columnCount)
    {
        matrix = new double[lineCount, columnCount];
        lines = lineCount;
        columns = columnCount;
        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                matrix[i, j] = 0;
            }
        }
    }

    public MyMatrix(int lineCount, int columnCount, int randBegin, int randEnd)
    {
        matrix = new double[lineCount, columnCount];
        lines = lineCount;
        columns = columnCount;
        Random rand = new Random();
        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                int rnd = rand.Next(randBegin, randEnd);
                matrix[i, j] = rnd;
            }
        }
    }

    public double this[int elem1, int elem2]
    {
        get { return matrix[elem1, elem2]; }
        set { matrix[elem1, elem2] = value; }
    }

    public void Fill()
    {
        Random rand = new Random();
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int rnd = rand.Next(0, 100);
                matrix[i, j] = rnd;
            }
        }
    }

    public void ChangeSize(int linesCount, int columnCounts)
    {
        double[,] tempMat = new double[linesCount, columnCounts];
        Random rand = new Random();
        for (int i = 0; i < linesCount; i++)
        {
            if (i < lines)
            {
                for (int j = 0; j < columnCounts; j++)
                {
                    if (j < columns)
                    {
                        tempMat[i, j] = matrix[i, j];
                    }
                    else
                    {
                        int rnd = rand.Next(0, 100);
                        tempMat[i, j] = rnd;
                    }
                }
            }
            else
            {
                for (int j = 0; j < columnCounts; j++)
                {
                    int rnd = rand.Next(0, 100);
                    tempMat[i, j] = rnd;
                }
            }
        }

        matrix = new double[linesCount, columnCounts];
        matrix = tempMat;
        lines = linesCount;
        columns = columnCounts;
    }

    public void Show()
    {
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }

            Console.WriteLine("\r");
        }

        Console.WriteLine("\r");
    }

    public void ShowPartial(int[] a, int[] b)
    {
        {
            for (int i = a[0]; i <= b[0]; i++)
            {
                for (int j = a[1]; j <= b[1]; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine("\r");
            }

            Console.WriteLine("\r");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyMatrix matrix = new MyMatrix(3, 3, 10, 20);
        matrix.Show();

        matrix[1, 1] = 0;
        matrix.Show();

        matrix.Fill();
        matrix.Show();

        matrix.ChangeSize(5, 5);
        matrix.Show();

        matrix.Fill();
        matrix.Show();

        int[] a = new int[] { 0, 0 };
        int[] b = new int[] { 3, 2 };
        matrix.ShowPartial(a, b);


        int lines;
        int columns;
        int randBegin;
        int randEnd;

        Console.WriteLine("Введите количтество столбцов: ");
        lines = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите количтество строк: ");
        columns = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите минимальное значение элементов:");
        randBegin = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите максимальное значение элементов:");
        randEnd = int.Parse(Console.ReadLine());

        MyMatrix myMatrix = new MyMatrix(lines, columns, randBegin, randEnd);
        myMatrix.Show();

        Console.WriteLine("Введите новое количтество столбцов: ");
        lines = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите новое количтество строк: ");
        columns = int.Parse(Console.ReadLine());

        myMatrix.ChangeSize(lines, columns);
        myMatrix.Show();
    }
}