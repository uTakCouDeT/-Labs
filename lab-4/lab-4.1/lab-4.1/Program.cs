public class MyMatrix
{
    readonly double[,] _matrix;
    readonly int _lines;
    readonly int _columns;

    public MyMatrix(int m, int n)
    {
        _matrix = new double[m, n];
        _lines = m;
        _columns = n;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _matrix[i, j] = 0;
            }
        }
    }

    public MyMatrix(int m, int n, int rand1, int rand2)
    {
        _matrix = new double[m, n];
        _lines = m;
        _columns = n;
        Random rand = new Random();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _matrix[i, j] = rand.Next(rand1, rand2);
            }
        }
    }

    public double this[int a, int b]
    {
        get { return _matrix[a, b]; }
        set { _matrix[a, b] = value; }
    }

    public static MyMatrix operator +(MyMatrix lhs, MyMatrix rhs)
    {
        MyMatrix newMatrix = new MyMatrix(lhs._lines, lhs._columns);
        for (int i = 0; i < lhs._lines; i++)
        {
            for (int j = 0; j < lhs._columns; j++)
            {
                newMatrix[i, j] = lhs[i, j] + rhs[i, j];
            }
        }

        return newMatrix;
    }

    public static MyMatrix operator -(MyMatrix lhs, MyMatrix rhs)
    {
        MyMatrix newMatrix = new MyMatrix(lhs._lines, lhs._columns);
        for (int i = 0; i < lhs._lines; i++)
        {
            for (int j = 0; j < lhs._columns; j++)
            {
                newMatrix[i, j] = lhs[i, j] - rhs[i, j];
            }
        }

        return newMatrix;
    }

    static public MyMatrix operator *(MyMatrix lhs, MyMatrix rhs)
    {
        MyMatrix newMatrix = new MyMatrix(lhs._lines, lhs._columns);
        for (int i = 0; i < lhs._lines; i++)
        {
            for (int j = 0; j < lhs._columns; j++)
            {
                newMatrix[i, j] = 0;
                for (int k = 0; k < lhs._columns; k++)
                {
                    newMatrix[i, j] += lhs[i, k] * rhs[k, j];
                }
            }
        }

        return newMatrix;
    }

    static public MyMatrix operator *(MyMatrix matrix, double num)
    {
        MyMatrix newMatrix = new MyMatrix(matrix._lines, matrix._columns);
        for (int i = 0; i < matrix._lines; i++)
        {
            for (int j = 0; j < matrix._columns; j++)
            {
                newMatrix[i, j] = matrix[i, j] * num;
            }
        }

        return newMatrix;
    }

    static public MyMatrix operator *(double num, MyMatrix matrix)
    {
        return matrix * num;
    }

    static public MyMatrix operator /(MyMatrix matrix, double num)
    {
        MyMatrix newMatrix = new MyMatrix(matrix._lines, matrix._columns);
        for (int i = 0; i < matrix._lines; i++)
        {
            for (int j = 0; j < matrix._columns; j++)
            {
                newMatrix[i, j] = matrix[i, j] / num;
            }
        }

        return newMatrix;
    }

    public void Print()
    {
        for (int i = 0; i < _lines; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Console.Write($"{_matrix[i, j]}  ");
            }

            Console.WriteLine();
        }
    }
}


internal class Program
{
    public static void Main(string[] args)
    {
        int line;
        int column;
        int rand1;
        int rand2;

        Console.WriteLine("Введите количество строк:");
        line = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов:");
        column = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Введите минимальное значение элементов:");
        rand1 = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение элементов:");
        rand2 = int.Parse(Console.ReadLine());

        MyMatrix matrix1 = new MyMatrix(line, column, rand1, rand2);
        MyMatrix matrix2 = new MyMatrix(line, column, rand1, rand2);
        
        MyMatrix newMatrix = new MyMatrix(line, column);

        Console.WriteLine("Первая матрица:");
        matrix1.Print();

        Console.WriteLine("Вторая матрица:");
        matrix2.Print();


        Console.WriteLine("Сложение матриц:");
        newMatrix = matrix1 + matrix2;
        newMatrix.Print();

        Console.WriteLine("Вычитание матриц:");
        newMatrix = matrix1 - matrix2;
        newMatrix.Print();

        Console.WriteLine("Умножение матрицы на матрицу:");
        newMatrix = matrix1 * matrix2;
        newMatrix.Print();

        Console.WriteLine("Введите число на которое вы хотите умножить/разделить матрицу:");
        int num = int.Parse(Console.ReadLine());

        Console.WriteLine("Умножение матрицы на число:");
        newMatrix = matrix1 * num;
        newMatrix.Print();

        Console.WriteLine("Деление матрицы на число:");
        newMatrix = matrix1 / num;
        newMatrix.Print();
    }
}