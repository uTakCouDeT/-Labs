public class Vector
{
    public double X;
    public double Y;
    public double Z;

    public Vector(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    private double Len()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }
    
    public void Print()
    {
        Console.WriteLine($"({X}, {Y}, {Z})");
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static double operator *(Vector v1, Vector v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    public static Vector operator *(Vector v1, double num)
    {
        return new Vector(v1.X * num, v1.Y * num, v1.Z * num);
    }

    public static bool operator ==(Vector v1, Vector v2)
    {
        return Equals(v1.Len(), v2.Len());
    }

    public static bool operator !=(Vector v1, Vector v2)
    {
        return !Equals(v1.Len(), v2.Len());
    }

    public static bool operator >(Vector v1, Vector v2)
    {
        return v1.Len() > v2.Len();
    }

    public static bool operator <(Vector v1, Vector v2)
    {
        return v1.Len() < v2.Len();
    }
    
    public static bool operator >=(Vector v1, Vector v2)
    {
        return v1.Len() >= v2.Len();
    }
    
    public static bool operator <=(Vector v1, Vector v2)
    {
        return v1.Len() <= v2.Len();
    }
}

internal class Program
{
    public static void Main(string[] args)
    {
        Vector v1 = new(7, 7, 7);
        Vector v2 = new(1, 2, 3);

        Console.Write("v1: ");
        v1.Print();
        Console.Write("v2: ");
        v2.Print();
        
        var v3 = v1 + v2;
        Console.Write("v1 + v2: ");
        v3.Print();
        
        v3 = v1 * 2;
        Console.Write($"v1 * 2: ");
        v3.Print();

        Console.WriteLine($"v1 * v2 = {v1 * v2}");
        Console.WriteLine($"v1 != v2: {v1 != v2}");
        Console.WriteLine($"v1 > v2: {v1 > v2}");
        Console.WriteLine($"v1 <= v2: {v1 <= v2}");
    }
}