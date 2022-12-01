using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallel
{
    class Program
	{
		public static void Main(string[] args)
		{
			// 传入一个 lambda 表达式作为委托
			Task<int> task1 = CreateTask("Task1");
			task1.Start();
			Console.WriteLine($"Result1: {task1.Result}");
			
			// 主线程会等待，直到任务返回前一直处于阻塞状态。
			Task<int> task2 = CreateTask("Task2");
			task2.RunSynchronously(); // 避免使用线程池来执行非常短暂的操作。
			Console.WriteLine($"Result2: {task2.Result}");

			Task<int> task3 = CreateTask("Task3");
			task3.Start();
			while(!task3.IsCompleted)
			{
				Console.WriteLine(task3.Status);
				Thread.Sleep(TimeSpan.FromSeconds(0.5));
			}
			Console.WriteLine(task3.Status);
			Console.WriteLine($"Result3: {task3.Result}");

			Console.ReadLine();
		}

		public static Task<int> CreateTask(string name)
		{
			//Task task1 = new Task(() => TestMethod("Task 1"));
			//Task task2 = new Task(new Action(() => TestMethod("Task 2")));
			//task1.Start(); // 以构造函数的方式调用任务，不要忘记执行 Start 方法。
			//task2.Start(); // 以构造函数的方式调用任务，不要忘记执行 Start 方法。
			//Task.Run(() => TestMethod("Task 3"));
			//Task.Factory.StartNew(() => TestMethod("Task 4"));
			//Task.Factory.StartNew(() => TestMethod("Task 5"), TaskCreationOptions.LongRunning);
			return new Task<int>(() => TestMethod(name));
		}

		public static int TestMethod(string name)
		{
			Console.WriteLine("{0} is running on a thread id {1}. Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
			Thread.Sleep(TimeSpan.FromSeconds(2));
			return 42;
		}
	}
}
