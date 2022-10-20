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

    public void ShowPartial(int[] firstCoord, int[] secCoord)
    {
        for (int i = firstCoord[0]; i <= secCoord[0]; ++i)
        {
            if (i == firstCoord[0] && firstCoord[0] == secCoord[0])
            {
                for (int j = firstCoord[1]; j <= secCoord[1]; ++j)
                {
                    Console.WriteLine(matrix[i, j]);
                }

                break;
            }
            else if (i == firstCoord[0])
            {
                for (int j = firstCoord[1]; j < columns; ++j)
                {
                    Console.WriteLine(matrix[i, j]);
                }
            }
            else if (i < secCoord[0])
            {
                for (int j = 0; j < columns; ++j)
                {
                    Console.WriteLine(matrix[i, j]);
                }
            }
            else if (i == secCoord[0])
            {
                for (int j = 0; j <= secCoord[1]; ++j)
                {
                    Console.WriteLine(matrix[i, j]);
                }
            }
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyMatrix xi = new MyMatrix(3, 3, 10, 20);
        xi.Show();

        xi[0, 0] = 5;
        xi.Show();

        xi.Fill();
        xi.Show();

        xi.ChangeSize(5, 5);
        xi.Show();

        xi.Fill();
        xi.Show();

        int[] a = new int[] { 1, 2 };
        int[] b = new int[] { 2, 3 };
        xi.ShowPartial(a, b);


        int lines;
        int columns;
        int firstArg;
        int secondArg;

        Console.WriteLine("Количтество столбцов: ");
        lines = int.Parse(Console.ReadLine());
        Console.WriteLine("Количтество строк: ");
        columns = int.Parse(Console.ReadLine());
        Console.WriteLine("Нижняя граница слуайных чисел: ");
        firstArg = int.Parse(Console.ReadLine());
        Console.WriteLine("Верхняя граница слуайных чисел: ");
        secondArg = int.Parse(Console.ReadLine());

        MyMatrix newMat = new MyMatrix(lines, columns, firstArg, secondArg);
        newMat.Show();

        Console.WriteLine("Ихменим размер матрицы! ");
        Console.WriteLine("Количтество столбцов: ");
        lines = int.Parse(Console.ReadLine());
        Console.WriteLine("Количтество строк: ");
        columns = int.Parse(Console.ReadLine());

        newMat.ChangeSize(lines, columns);
        newMat.Show();
    }
}