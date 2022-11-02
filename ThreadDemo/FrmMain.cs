﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// 非阻塞式编程：单线程异步
// 阻塞式编程：多线程同步

// 阻塞式操作
// - 读写文件
// - 网络请求
// - 访问数据库

// 一个进程的多个线程可以运行在不同的CPU上，或者多核CPU的不同核上。
// 多个线程访问同一块数据，需要添加同步机制。

// 并行两种场景：任务并行，数据并行。

// 前台线程和后台线程

// 线程调用 Sleep 休眠或者等待其它线程执行结束，线程暂停执行。线程会阻塞代码继续往下运行。

namespace ThreadDemo
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
			RaceConditions();
		}

		// 线程优先级，线程调度器，线程调度队列
		public static void TestThreadPriority()
		{
			List<ThreadPriority> list = new List<ThreadPriority>() 
			{
				ThreadPriority.Lowest,
				ThreadPriority.BelowNormal,
				ThreadPriority.Normal,
				ThreadPriority.AboveNormal,
				ThreadPriority.Highest,
			};
			for (int i = 0; i < list.Count; i++)
			{
				var index = i;
				new Thread(() =>
				{
					Thread.Sleep(100);
					LogHelper.Log($"Thread-{index + 1}");
				}) { Priority = list[index] }.Start();
			}
		}

		public static void TestParallel1()
		{
			// 不理解
			// Parallel.Invoke 用于任务并行性 和 Parallel.ForEach 用于数据并行性。
			Parallel.For(0, 10, i =>
			{
				LogHelper.Log($"{i}, task: {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");
				Thread.Sleep(10);
			});

			Parallel.For(0, 10, async i =>
			{
				LogHelper.Log($"{i}, task: {Task.CurrentId}, thread: {Thread.CurrentThread.ManagedThreadId}");
				await Task.Delay(10);
			});
		}

		public static void TestParallel2()
		{
			// 预期中并不是先打印三次 yyyy，然后打印线程执行结束。
			Thread thread = new Thread(Print);
			thread.Start();
			LogHelper.Log("线程执行结束");
		}

		public static void TestParallel3()
		{
			// 线程汇合
			Thread thread = new Thread(Print);
			thread.Start();
			thread.Join();
			LogHelper.Log("线程执行结束");
		}

		public static void TestParallel4()
		{
			// 线程汇合
			Thread thread1 = new Thread(Print1);
			Thread thread2 = new Thread(Print2);
			thread1.Start();
			thread2.Start();
			// thread1.Join();
			thread2.Join();
			LogHelper.Log("线程执行结束");
		}

		public static void Print()
		{
			for (int i = 0; i < 3; i++)
			{
				LogHelper.Log("yyyy");
			}
		}

		public static void Print1()
		{
			Thread.Sleep(1500);
			LogHelper.Log("I want sleep --- 1");
		}

		public static void Print2()
		{
			Thread.Sleep(1000);
			LogHelper.Log("I want sleep --- 2");
		}

		public static void Print3()
		{
			LogHelper.Log("I want sleep");
		}

		public static void TestTask()
		{

		}

		public static void Cancelllation()
		{

		}

		public static void TestThreadClass()
		{

		}

		public static void Synchronization()
		{

		}

		public static void TestDataFlow()
		{

		}

		public static void TestSecurity()
		{
			Thread thread = new Thread(ThreadSecurity.Print);
			thread.Start();
			ThreadSecurity.Print();
			LogHelper.Log($"count: {ThreadSecurity.Count}");
		}

		class ThreadSecurity
		{
			private static bool completed = false;

			private static int _count = 0;

			public static int Count
			{
				get
				{
					return _count;
				}
			}

			public static void Print()
			{
				if (!completed)
				{
					completed = true;
					_count++;
				}
			}

			public static void Go()
			{
				if (!completed)
				{
					completed = true;
					_count++;
				}
			}
		}

		public static void TestSecurity2()
		{

		}

		public static void Go()
		{

		}

		public static void TestThreadPool()
		{
			int workerThreads;
			int ioThreads;
			ThreadPool.GetMaxThreads(out workerThreads, out ioThreads);
			LogHelper.Log($"Max worker threads: {workerThreads}, I/O threads: {ioThreads}");
			for (int i = 0; i < 16; i++)
			{
				ThreadPool.QueueUserWorkItem(JobForAThread);
			}
		}

		public static void JobForAThread(object state)
		{
			for (int i = 0; i < 2047; i++)
			{
				LogHelper.Log($"loop: {i}, running inside pooled thread: {Thread.CurrentThread.ManagedThreadId}");
				Thread.Sleep(500);
			}
		}

		public static void ThreadMain()
		{
			// 默认情况下使用 Thread 创建的线程是前台线程。线程池中的线程总是后台线程。

			var thread = new Thread(() =>
			{
				LogHelper.Log($"Thread Backend Running {Thread.CurrentThread.Name}");
			})
			{
				IsBackground = false // 默认是前台线程
			};
			thread.Start();
			LogHelper.Log("Thread Frontend Running");

			// 通过 start 方法传递参数
			new Thread(ThreadWithParams).Start(new Data()
			{
				Message = "Info"
			});

			MyThread myThread = new MyThread("Hello");
			new Thread(myThread.MainThread).Start();
		}

		struct Data
		{
			public string Message;
		}

		// ParameterizedThreadStart 委托
		// 必须有一个 object 类型的参数，而且返回类型必须是 void
		public static void ThreadWithParams(object obj)
		{
			Data data = (Data)obj;
			LogHelper.Log($"Running in a thread, received {data.Message}");
		}

		public class MyThread
		{
			private string data;

			public MyThread(string mesg)
			{
				data = mesg;
			}

			public void MainThread()
			{
				LogHelper.Log($"Running in a thread, data: {data}");
			}
		}

		public class StateObject
		{
			private int state = 5;

			public void ChangeState(int loop)
			{
				if (state == 5)
				{
					state++;
					if (state == 7)
					{
						LogHelper.Log("Race Condition occurred after " + loop + " loops");
					}
				}
				state = 5;
			}
		}

		public class SimpleTask
		{
			public void RaceCondition(object obj)
			{
				Trace.Assert(obj is StateObject, "obj must be type of StateObject");
				StateObject state = obj as StateObject;
				int i = 0;
				while(true)
				{
					state.ChangeState(i++);
				}
			}
		}

		public class SafetyTask
		{
			public void RaceCondition(object obj)
			{
				Trace.Assert(obj is StateObject, "obj must be type of StateObject");
				StateObject state = obj as StateObject;
				int i = 0;
				while (true)
				{
					lock (state)
					{
						state.ChangeState(i++);
					}
				}
			}
		}

		public static void RaceConditions()
		{
			// 没有对 state1 进行防护，存在条件竞争。
			var state1 = new StateObject();
			for(int i = 0; i < 2; i++)
			{
				Task.Run(() => new SimpleTask().RaceCondition(state1));
			}
			// 对 state2 进行了防护，不存在条件竞争。
			var state2 = new StateObject();
			for (int i = 0; i < 2; i++)
			{
				Task.Run(() => new SafetyTask().RaceCondition(state2));
			}
		}
	}
}
