using System;


public struct Point3D
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public Point3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    
    public override string ToString() => $"({X:F1}, {Y:F1}, {Z:F1})";
}
public static Point3D operator -(Point3D p1, Point3D p2)
{
    return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
}

public static double DotProduct(Point3D p1, Point3D p2)
{
    return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
}

public static Point3D CrossProduct(Point3D p1, Point3D p2)
{
    return new Point3D(
        p1.Y * p2.Z - p1.Z * p2.Y,
        p1.Z * p2.X - p1.X * p2.Z,
        p1.X * p2.Y - p1.Y * p2.X
    );
}
public double CalculateVolume()
{
    var ab = B - A;
    var ac = C - A;
    var ad = D - A;

    var crossProduct = Point3D.CrossProduct(ac, ad);
    double mixedProduct = Point3D.DotProduct(ab, crossProduct);

    return Math.Abs(mixedProduct) / 6.0;
}


public class Tetrahedron
{
    
    public Point3D A { get; }
    public Point3D B { get; }
    public Point3D C { get; }
    public Point3D D { get; }

    
    public Tetrahedron(Point3D a, Point3D b, Point3D c, Point3D d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    
    public double CalculateVolume()
    {
       
        double v1x = B.X - A.X;
        double v1y = B.Y - A.Y;
        double v1z = B.Z - A.Z;

        double v2x = C.X - A.X;
        double v2y = C.Y - A.Y;
        double v2z = C.Z - A.Z;

        double v3x = D.X - A.X;
        double v3y = D.Y - A.Y;
        double v3z = D.Z - A.Z;

        
        double mixedProduct = v1x * (v2y * v3z - v2z * v3y) -
                              v1y * (v2x * v3z - v2z * v3x) +
                              v1z * (v2x * v3y - v2y * v3x);

        
        return Math.Abs(mixedProduct) / 6.0;
    }
Point3D ReadPoint(string pointName)
{
    double x, y, z;
    while (true)
    {
        Console.WriteLine($"Введіть координати {pointName} (формат: X Y Z): ");
        var input = Console.ReadLine().Split(' ');

        if (input.Length == 3 &&
            double.TryParse(input[0], NumberStyles.Any, CultureInfo.InvariantCulture, out x) &&
            double.TryParse(input[1], NumberStyles.Any, CultureInfo.InvariantCulture, out y) &&
            double.TryParse(input[2], NumberStyles.Any, CultureInfo.InvariantCulture, out z))
        {
            return new Point3D(x, y, z);
        }
        else
        {
            Console.WriteLine("Некоректний ввід! Спробуйте ще раз.");
        }
    }
}

    
    public override string ToString()
    {
        return $"Тетраедр з вершинами: A{A}, B{B}, C{C}, D{D}";
    }
}


public class Program
{
    public static void Main(string[] args)
    {
       
        Random random = new Random();

        
        Console.Write("Введіть кількість тетраедрів для генерації (n): ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Помилка. Будь ласка, введіть додатне ціле число.");
            Console.Write("Введіть кількість тетраедрів (n): ");
        }

       
        Tetrahedron[] tetrahedrons = new Tetrahedron[n];

       
        Point3D CreateRandomPoint()
        {
            double x = random.NextDouble() * 20 - 10; 
            double y = random.NextDouble() * 20 - 10;
            double z = random.NextDouble() * 20 - 10;
            return new Point3D(x, y, z);
        }

        
        for (int i = 0; i < n; i++)
        {
            tetrahedrons[i] = new Tetrahedron(
                CreateRandomPoint(),
                CreateRandomPoint(),
                CreateRandomPoint(),
                CreateRandomPoint()
            );
        }

        Console.WriteLine($"\nСтворено та заповнено масив з {n} випадкових тетраедрів.");
        Console.WriteLine("--------------------------------------------------\n");

        double maxVolume = -1.0;
        int maxIndex = -1;

       
        for (int i = 0; i < tetrahedrons.Length; i++)
        {
            double currentVolume = tetrahedrons[i].CalculateVolume();
            Console.WriteLine($"Тетраедр №{i + 1}: {tetrahedrons[i]}");
            Console.WriteLine($"--> Об'єм: {currentVolume:F4}\n");

           
            if (currentVolume > maxVolume)
            {
                maxVolume = currentVolume;
                maxIndex = i;
            }
        }

        
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("🏆 Тетраедр з найбільшим об'ємом:");
        if (maxIndex != -1)
        {
            Console.WriteLine($"Порядковий номер в масиві: {maxIndex + 1}");
            Console.WriteLine(tetrahedrons[maxIndex].ToString());
            Console.WriteLine($"Найбільший об'єм: {maxVolume:F4}");
        }
        else
        {
            Console.WriteLine("Масив порожній, не вдалося знайти тетраедр.");
        }
    }
}
