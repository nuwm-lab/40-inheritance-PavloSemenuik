using System;
public class QuadraticEquation
{
    // Коефіцієнти квадратного рівняння
    protected double b2, b1, b0;

    // Метод введення коефіцієнтів
    public void SetCoefficients()
    {
        Console.WriteLine("Введiть коефiцiєнт b2:");
        b2 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введiть коефiцiєнт b1:");
        b1 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введiть коефiцiєнт b0:");
        b0 = double.Parse(Console.ReadLine());
    }
    // Метод виведення коефіцієнтів
    public void PrintCoefficients()
    {
        Console.WriteLine($"Коефiцiєнти квадратного рiвняння: b2 = {b2}, b1 = {b1}, b0 = {b0}");
    }
    // Метод для перевірки, чи задовольняє x рівняння
    public bool CheckSolution(double x)
    {
        return Math.Abs(b2 * x * x + b1 * x + b0) < 1e-6;
    }
    // Метод для знаходження коренів квадратного рівняння
    public void Solve()
    {
        double discriminant = b1 * b1 - 4 * b2 * b0;

        if (discriminant < 0)
        {
            Console.WriteLine("Рiвняння не має коренiв (дискримiнант < 0).");
        }
        else if (Math.Abs(discriminant) < 1e-6)
        {
            double root = -b1 / (2 * b2);
            root = Math.Round(root, 6); // Округлення кореня
            Console.WriteLine($"Рiвняння має один корiнь: x = {root}");
        }
        else
        {
            double root1 = (-b1 + Math.Sqrt(discriminant)) / (2 * b2);
            double root2 = (-b1 - Math.Sqrt(discriminant)) / (2 * b2);

            root1 = Math.Round(root1, 6); // Округлення першого кореня
            root2 = Math.Round(root2, 6); // Округлення другого кореня

            Console.WriteLine($"Рiвняння має два коренi: x1 = {root1}, x2 = {root2}");
        }
    }
}

// Клас для кубічного рівняння
public class CubicEquation : QuadraticEquation
{
    // Додатковий коефіцієнт a3 для кубічного рівняння
    private double a3;

    // Перевизначення методу введення коефіцієнтів
    public new void SetCoefficients()
    {
        Console.WriteLine("Введiть коефiцiєнт a3:");
        a3 = double.Parse(Console.ReadLine());
        base.SetCoefficients();
    }

    // Перевизначення методу виведення коефіцієнтів
    public new void PrintCoefficients()
    {
        Console.WriteLine($"Коефiцiєнти кубiчного рiвняння: a3 = {a3}, b2 = {b2}, b1 = {b1}, b0 = {b0}");
    }

    // Метод для знаходження коренів кубічного рівняння
    public void Solve()
    {
        if (Math.Abs(a3) < 1e-6)
        {
            // Якщо a3 = 0, рівняння стає квадратним
            Console.WriteLine("Це не кубiчне рiвняння. Розв'язуємо як квадратне.");
            base.Solve();
            return;
        }

        // Нормалізація коефіцієнтів (зробимо a3 = 1)
        double p = b2 / a3;
        double q = b1 / a3;
        double r = b0 / a3;

        // Знаходимо дискримінант
        double delta = (q / 3) - (p * p / 9);
        double h = r / 2 - (p * q / 6) + (p * p * p / 27);
        double discriminant = delta * delta * delta + h * h;

        Console.WriteLine($"Дискримінант кубічного рівняння: {Math.Round(discriminant, 6)}");

        if (Math.Abs(discriminant) < 1e-6)
        {
            // Один дійсний корінь
            double root = Math.Cbrt(-h) - p / 3;
            root = Math.Round(root, 6); // Округлення кореня
            Console.WriteLine($"Кубічне рівняння має один дійсний корінь: x = {root}");
        }
        else if (discriminant > 0)
        {
            // Один дійсний корінь і два комплексні
            double sqrtD = Math.Sqrt(discriminant);
            double u = Math.Cbrt(-h + sqrtD);
            double v = Math.Cbrt(-h - sqrtD);
            double root = u + v - p / 3;

            root = Math.Round(root, 6); // Округлення кореня
            Console.WriteLine($"Кубічне рівняння має один дійсний корінь: x = {root}");
        }
        else
        {
            // Три дійсні корені
            double phi = Math.Acos(-h / Math.Sqrt(-delta * delta * delta));
            double root1 = 2 * Math.Sqrt(-delta) * Math.Cos(phi / 3) - p / 3;
            double root2 = 2 * Math.Sqrt(-delta) * Math.Cos((phi + 2 * Math.PI) / 3) - p / 3;
            double root3 = 2 * Math.Sqrt(-delta) * Math.Cos((phi + 4 * Math.PI) / 3) - p / 3;

            root1 = Math.Round(root1, 6); // Округлення першого кореня
            root2 = Math.Round(root2, 6); // Округлення другого кореня
            root3 = Math.Round(root3, 6); // Округлення третього кореня

            Console.WriteLine($"Кубічне рівняння має три дійсні корені: x1 = {root1}, x2 = {root2}, x3 = {root3}");
        }
    }

    // Перевизначення методу для перевірки розв'язку
    public new bool CheckSolution(double x)
    {
        return Math.Abs(a3 * x * x * x + b2 * x * x + b1 * x + b0) < 1e-6;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Оберiть тип рiвняння: (1 - квадратне, 2 - кубiчне)");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            // Робота з квадратним рівнянням
            QuadraticEquation quadratic = new QuadraticEquation();
            Console.WriteLine("Введення коефiцiєнтів для квадратного рiвняння:");
            quadratic.SetCoefficients();
            quadratic.PrintCoefficients();
            quadratic.Solve();
            Console.WriteLine("Введiть значення x для перевiрки:");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine($"Число x = {x} задовольняє рiвнянню? {quadratic.CheckSolution(x)}");
        }
        else if (choice == 2)
        {
            // Робота з кубічним рівнянням
            CubicEquation cubic = new CubicEquation();
            Console.WriteLine("Введення коефiцiєнтів для кубiчного рiвняння:");
            cubic.SetCoefficients();
            cubic.PrintCoefficients();
            cubic.Solve();
            Console.WriteLine("Введiть значення x для перевiрки:");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine($"Число x = {x} задовольняє рiвнянню? {cubic.CheckSolution(x)}");
        }
        else
        {
            Console.WriteLine("Неправильний вибiр!");
        }
    }
}
