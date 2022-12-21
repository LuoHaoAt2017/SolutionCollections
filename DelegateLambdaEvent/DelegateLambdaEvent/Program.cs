// 通过 lambda 表达式实现委托调用

// 当方法作为参数传递给其它方法时，需要使用委托。

public class Program
{
    public delegate void PrintDelegate(string mesg);

    public static void Main()
    {
        // 将方法 Print 作为参数传递给方法 Calculate。
        Calculate(1, 1, Print);
    }

    public static void Calculate(int x, int y, PrintDelegate cb)
    {
        int result = x + y;
        cb($"the result is {result}");
    }

    public static void Print(string mesg)
    {
        Console.WriteLine(mesg);
    }
}