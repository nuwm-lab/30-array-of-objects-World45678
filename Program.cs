public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Введіть кількість тетраедрів для генерації (n): ");
        int n = int.Parse(Console.ReadLine());

        Tetrahedron[] tetrahedrons = new Tetrahedron[n];

        for (int i = 0; i < n; i++)
        {
            var a = ReadPoint("A");
            var b = ReadPoint("B");
            var c = ReadPoint("C");
            var d = ReadPoint("D");
            tetrahedrons[i] = new Tetrahedron(a, b, c, d);
        }

        double maxVolume = -1;
        int maxIndex = -1;

        for (int i = 0; i < n; i++)
        {
            double volume = tetrahedrons[i].CalculateVolume();
            Console.WriteLine($"Тетраедр {i + 1} об'єм: {volume:F4}");

            if (volume > maxVolume)
            {
                maxVolume = volume;
                maxIndex = i;
            }
        }

        Console.WriteLine($"Тетраедр з найбільшим об'ємом: {maxIndex + 1}, об'єм: {maxVolume:F4}");
    }
}
public struct Point3D
{
    // поля і конструктор

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
}
