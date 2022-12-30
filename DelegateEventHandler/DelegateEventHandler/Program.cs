// 事件的处理程序和事件的委托必须具有相同的参数和返回类型
// IncreaseDozenCount 和 Handler 必须具有相同的参数和返回类型
delegate void Handler();

class Incrementer
{
    // 需要声明为 public
    // 事件关键字 event
    // 委托类型是 Handler
    // 创事件名 CountedADozen
    // 事件是类的成员
    public event Handler CountedADozen;

    public void DoCount()
    {
        for (int i = 0; i < 100; i++)
        {
            if (i > 0 && i % 12 == 0)
            {
                // 在触发事件之前，需要判断事件处理程序不为空
                if (CountedADozen != null)
                {
                    // 触发事件的代码
                    CountedADozen();
                }
            }
        }
    }
}

class Dozens
{
    public int DozenCount { get; private set; }

    public Dozens(Incrementer incrementer)
    {
        DozenCount = 0;
        // 订阅者订阅事件，当事件发生时得到通知。
        incrementer.CountedADozen += IncreaseDozenCount;
    }

    // 声明事件处理程序
    public void IncreaseDozenCount()
    {
        this.DozenCount++;
    }
}

class Puber
{
    public event EventHandler<CustomEventArgs>? CustomClick;

    public void Trigger()
    {
        if (CustomClick != null)
        {
            CustomEventArgs args = new CustomEventArgs();
            args.message = "Hello World";
            CustomClick(this, args);
        }
    }
}

class Suber
{
    public Suber(Puber puber)
    {
        puber.CustomClick += HandleClick;
    }

    public void HandleClick(object? sender, CustomEventArgs e)
    {
        Console.WriteLine($"Get Message from sender: {e.message}");
    }
}

// 事件处理参数
class CustomEventArgs: EventArgs
{
    public string? message { set; get; }
}

public class Program
{
    public static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        //Incrementer incrementer = new Incrementer();
        //Dozens dozens = new Dozens(incrementer);
        //incrementer.DoCount();
        //Console.WriteLine($"DozenCount: {dozens.DozenCount}");
        
        Puber puber = new Puber();
        Suber suber1 = new Suber(puber);
        Suber suber2 = new Suber(puber);
        Suber suber3 = new Suber(puber);
        puber.Trigger();

        Console.ReadLine();
    }
}
