// 通过 lambda 表达式实现委托调用

// 当方法作为参数传递给其它方法时，需要使用委托。

public class Program
{
    public delegate void PrintDelegate(string mesg);

    public delegate void CustomDelegate(string arg);

    public delegate string GetString();

    public event EventHandler? CustomEvent;

    public event CustomDelegate? CustomSingleClickEvent;

    public event CustomDelegate? CustomDoubleClickEvent;
                
    public static void Main()
    {
        // 将方法 Print 作为参数传递给方法 Calculate。
        // Calculate(1, 1, Print);

        Test6();

        Console.ReadLine();
    }

    // 将委托传递给方法
    public static void Calculate(int x, int y, PrintDelegate cb)
    {
        int result = x + y;
        cb($"the result is {result}");
    }

    public static void Print(string mesg)
    {
        Console.WriteLine(mesg);
    }

    public static void Test1()
    {
        // 将 a.ToString 委托给 getString。
        // 通过 getString.Invoke() 的方式调用 a.ToString()
        int a = 1;
        GetString getString = new GetString(a.ToString);
        Console.WriteLine(getString.Invoke());
    }

    public static void Test2()
    {
        DoubleOp[] ops = new DoubleOp[2]
        {
            SquareX, DoubleX
        };

        foreach (DoubleOp op in ops)
        {
            ProcessAndDisplay(op, 3);
        }
    }

    public static void Test3()
    {
        // Action<T> 允许调用不带返回值的方法
        // Func<T> 允许调用带返回值的方法
        Func<double, double>[] funcs = new Func<double, double>[2] { SquareX, DoubleX };
        foreach (Func<double, double> func in funcs)
        {
            ProcessAndDisplay(func, 3);
        }
    }

    public static void Test4()
    {
        Func<double, double, double> func = new Func<double, double, double>(ActionX);
        ProcessAndDisplay(func, 2, 3);
    }

    public static void Test5()
    {
        Action<double> action1 = new Action<double>(ActionX);
        ProcessAndDisplay(action1, 2);

        Action<double, double> action2 = new Action<double, double>(ActionY);
        ProcessAndDisplay(action2, 2, 3);
    }

    public static void Test6()
    {
        List<Emploee> emploees = new List<Emploee>(3)
        {
            new Emploee("tom", 1200),
            new Emploee("jim", 1500),
            new Emploee("cat", 1000),
        };
        BubbleSort(emploees, Emploee.compareTo);
        foreach (Emploee emploee in emploees)
        {
            Console.WriteLine(emploee.ToString());
        }
    }

    public delegate bool CompareToDelegate<T>(T elem1, T elem2);

    public static void BubbleSort<T>(T[] sortArray, CompareToDelegate<T> compareTo)
    {
        bool swapped = false;
        do
        {
            swapped = false;
            for (int i = 0; i < sortArray.Length - 1; i++)
            {
                if (compareTo(sortArray[i], sortArray[i + 1]) == true)
                {
                    T temp = sortArray[i];
                    sortArray[i] = sortArray[i+1];
                    sortArray[i + 1] = temp;
                    swapped = true;
                }
            }
        }
        while (swapped);
    }

    public static void BubbleSort<T>(List<T> sortArray, Func<T, T, bool> compareTo)
    {
        bool swapped = false;
        do
        {
            swapped = false;
            for (int i = 0; i < sortArray.Count - 1; i++)
            {
                if (compareTo(sortArray[i], sortArray[i + 1]) == true)
                {
                    T temp = sortArray[i];
                    sortArray[i] = sortArray[i + 1];
                    sortArray[i + 1] = temp;
                    swapped = true;
                }
            }
        }
        while (swapped);
    }

    public static double SquareX(double x)
    {
        return x * x;
    }

    public static double DoubleX(double x)
    {
        return x * 2;
    }

    public static void ActionX(double x)
    {
        Console.WriteLine($"x: {x}");
    }

    public static void ActionY(double x, double y)
    {
        Console.WriteLine($"{x} + {y} = {x + y}");
    }

    public static double ActionX(double x, double y)
    {
        return x + y;
    }

    public delegate double DoubleOp(double x);

    public static void ProcessAndDisplay(DoubleOp action, double value)
    {
        var result = action.Invoke(value);
        Console.WriteLine($"ProcessAndDisplay: {result}");
    }

    public static void ProcessAndDisplay(Func<double, double> func, double value)
    {
        Console.WriteLine($"ProcessAndDisplay: {func.Invoke(value)}");
    }

    public static void ProcessAndDisplay(Func<double, double, double> func, double value1, double value2)
    {
        double result = func(value1, value2);
        Console.WriteLine($"result: {result}");
    }

    public static void ProcessAndDisplay(Action<double> action, double value)
    {
        action.Invoke(value);
    }

    public static void ProcessAndDisplay(Action<double, double> action, double value1, double value2)
    {
        action.Invoke(value1, value2);
    }
}

public class Emploee
{
    public int salary;

    public string name;

    public Emploee(string name, int salary)
    {
        this.name = name;
        this.salary = salary;
    }

    override
    public string ToString()
    {
        return($"name: {name}, salary:{salary}");
    }

    public static bool compareTo(Emploee emploee1, Emploee emploee2)
    {
        return emploee1.salary > emploee2.salary;
    }
}