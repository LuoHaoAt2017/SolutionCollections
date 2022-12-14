using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

// 没有对竞争条件进行必要的锁定会出问题，错误对竞争条件进行锁定会导致死锁。
// 多个线程访问共享状态，由于错误地使用锁，导致每个线程都在等待对方解除对共享状态的锁定。
// 锁定顺序和锁定超时时间
// 最好不要在线程之间共享数据，尽量避免。无法避免的话，就必须采用同步技术。同步技术有很多，其中之一就是对争用条件加锁 lock。
// 同步技术：确保一次只有一个线程访问和改变共享状态。

// 锁定只能锁定对象的引用。锁定值得副本没有必要。锁定得对象还可以区分为：锁定实例成员，锁定静态成员。
namespace ThreadDemo
{
	public partial class FrmMain : Form
	{
		public delegate void DelegateRun();

		public delegate void DelegateEat(string food);

		public delegate DateTime DelegateSleep(DateTime date);

		public FrmMain()
		{
			InitializeComponent();
			TestThreadingTimer();
		}

		public static void TestAction()
		{
			
			Action func1 = Dog.Run;
			// Action func2 = Dog.Eat; Action 类型的委托不接受参数，也没有返回值。
			DelegateRun run = Dog.Run;
			DelegateEat eat = Dog.Eat;
			DelegateSleep sleep = Dog.Sleep;

			run();
			eat("meat");
			sleep(DateTime.Now);
		}

		class Dog
		{
			public static void Run()
			{
				LogHelper.Log($"dog run at {DateTime.Now.ToString()}");
			}

			public static void Eat(string food)
			{

			}

			public static DateTime Sleep(DateTime date)
			{
				return date;
			}
		}

		public void TestThreadingTimer()
		{
			int dueTime = 1000;
			int period = 3000;
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
			Button button1 = new Button();
			Button button2 = new Button();
			Button button3 = new Button();
			Button button4 = new Button();
			Label label1 = new Label();
			Label label2 = new Label();
			Label label3 = new Label();
			System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback((object state) =>
			{
				this.Invoke(new Action(() => { label1.Text = "当前时间" + DateTime.Now.ToString();  }));
			}), null, dueTime, period);

			label1.Size = new Size(120, 40);
			label2.Size = new Size(120, 40);
			label3.Size = new Size(120, 40);

			label1.Text = "当前时间" + DateTime.Now.ToString();
			label2.Text = "间隔时间" + period;
			label3.Text = "延迟时间" + dueTime;

			button1.Size = new Size(120, 40);
			button1.Text = "定时器间隔时间减小";
			button1.Cursor = Cursors.Hand;

			button2.Size = new Size(120, 40);
			button2.Text = "定时器延迟时间减小";
			button2.Cursor = Cursors.Hand;

			button3.Size = new Size(120, 40);
			button3.Text = "定时器暂停执行";
			button3.Cursor = Cursors.Hand;

			button4.Size = new Size(120, 40);
			button4.Text = "定时器立即执行";
			button4.Cursor = Cursors.Hand;

			flowLayoutPanel.AutoSize = true;
			flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
			flowLayoutPanel.Controls.AddRange(new Control[] { label1, label2, label3, button1, button2, button3, button4 });

			button1.Click += new EventHandler((object sender, EventArgs e) =>
			{
				this.Invoke(new Action(() => {
					if (period > 1000)
					{
						period -= 1000;
						timer.Change(dueTime, period);
						label2.Text = "间隔时间" + period;
						label3.Text = "延迟时间" + dueTime;
					}
				}));
			});

			button2.Click += new EventHandler((object sender, EventArgs e) =>
			{
				this.Invoke(new Action(() => {
					if (dueTime > 1000)
					{
						dueTime -= 1000;
						timer.Change(dueTime, period);
						label2.Text = "间隔时间" + period;
						label3.Text = "延迟时间" + dueTime;
					}
				}));
			});

			button3.Click += new EventHandler((object sender, EventArgs e) =>
			{
				this.Invoke(new Action(() => {
					dueTime = -1;
					period = 10000;
					timer.Change(dueTime, period);
					label2.Text = "间隔时间" + period;
					label3.Text = "延迟时间" + dueTime;
				}));
			});

			button4.Click += new EventHandler((object sender, EventArgs e) =>
			{
				this.Invoke(new Action(() => {
					dueTime = 0;
					period = 10000;
					timer.Change(dueTime, period);
					label2.Text = "间隔时间" + period;
					label3.Text = "延迟时间" + dueTime;
				}));
			});

			this.Controls.Add(flowLayoutPanel);
		}

		public void TestInvoke()
		{
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
			Button button = new Button();
			Label label = new Label();
			button.Size = new Size(90, 40);
			button.Text = "显示当前时间";
			button.Cursor = Cursors.Hand;
			label.Size = new Size(120, 40);
			flowLayoutPanel.AutoSize = true;
			flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
			flowLayoutPanel.Controls.AddRange(new Control[] { label, button });
			button.Click += new EventHandler((object sender, EventArgs e) =>
			{
				Thread thread = new Thread(new ParameterizedThreadStart(ChangeLabelText));
				thread.Name = "ChangeLabelThread";
				thread.Start(label);
			});

			this.Controls.Add(flowLayoutPanel);
		}

		public void ChangeLabelText(object obj)
		{
			Label label = (Label)(obj);
			this.Invoke(new Action(() =>
			{
				label.Text = DateTime.Now.ToString();
			}));
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
			private StateObject s1;

			private StateObject s2;

			public SimpleTask()
			{

			}

			public SimpleTask(StateObject s1, StateObject s2)
			{
				this.s1 = s1;
				this.s2 = s2;
			}

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

			public void DeadLock1()
			{
				int i = 0;
				while(true)
				{
					lock(s1)
					{
						lock(s2)
						{
							s1.ChangeState(i);
							s2.ChangeState(i++);
							LogHelper.Console($"still running {i}");
						}
					}
				}
			}

			public void DeadLock2()
			{
				int i = 0;
				while (true)
				{
					lock (s2)
					{
						lock (s1)
						{
							s1.ChangeState(i);
							s2.ChangeState(i++);
							LogHelper.Console($"still running {i}");
						}
					}
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

		public static void TestDeadLock()
		{
			var state1 = new StateObject();
			var state2 = new StateObject();
			new Task(() => new SimpleTask(state1, state2).DeadLock1()).Start();
			new Task(() => new SimpleTask(state1, state2).DeadLock2()).Start();
		}

		public class ShareState
		{
			private int state = 0;

			private Object root = new object();
			public int State
			{
				get { return state;  }
			}

			public int IncrementState()
			{
				lock(root)
				{
					return state++;
				}
			}
		}

		public class Job
		{
			ShareState shareState;

			public Job(ShareState state)
			{
				shareState = state;
			}

			public void DoTheJob()
			{
				for (int i = 0; i < 5000; i++)
				{
					lock (shareState) // 锁定实例成员
					{
						shareState.IncrementState();
					}
				}
			}
		}

		public static void TestJob()
		{
			int Count = 20;
			ShareState state = new ShareState();
			var tasks = new Task[Count];
			for(int i = 0; i < Count; i++)
			{
				tasks[i] = Task.Run(() => new Job(state).DoTheJob());
			}
			for (int i = 0; i < Count; i++)
			{
				tasks[i].Wait();
			}
			LogHelper.Log($"total run {state.State} times");
		}

		public class SimpleDemo
		{

			// 非同步版本
			public virtual bool IsSynchronized
			{
				get { return false; }
			}

			public virtual void DoThis()
			{

			}

			public virtual void DoThat()
			{

			}

			// 同步版本
			// 同步版本只负责线程安全，具体的业务逻辑放在非同步版本中执行。
			private class SynchronizedDemo : SimpleDemo
			{
				private object syncRoot = new object();

				private SimpleDemo syncDemo;

				public SynchronizedDemo(SimpleDemo demo)
				{
					this.syncDemo = demo;
				}

				public override bool IsSynchronized
				{
					get { return true; }
				}

				public static SimpleDemo Synchronized(SimpleDemo demo)
				{
					if (!demo.IsSynchronized)
					{
						return new SynchronizedDemo(demo);
					}
					return demo;
				}

				public override void DoThis()
				{
					lock (syncRoot)
					{
						this.syncDemo.DoThis();
					}
				}

				public override void DoThat()
				{
					lock (syncRoot)
					{
						this.syncDemo.DoThat();
					}
				}
			}
		}

		public static void TestTimer()
		{
			var obj = new Object(); // 任意类型的对象，TimeAction 回调方法中 obj 接收
			TimeSpan delay = TimeSpan.FromSeconds(2); // 启动定时器后，第一次调用回调函数的时间
			TimeSpan interval = TimeSpan.FromSeconds(3); // 回调的重复时间间隔，置为 -1 时，定时器只能执行一次。
			var timer = new System.Threading.Timer(ThreadingTimerAction, obj, delay, interval);
			Thread.Sleep(15000);
			timer.Dispose();
		}

		// 基于委托的机制
		public static void ThreadingTimerAction(object obj)
		{
			// TimeAction 需要满足 TimeAction 委托，返回类型是 void, 接收的参数类型是 Object
			LogHelper.Log($"System.Threading.Timer {DateTime.Now:T}");
		}

		// 基于事件的机制
		public static void TimersTimer()
		{
			var t1 = new System.Timers.Timer(1000); // interval
			t1.AutoReset = true; // 是否重置触发
			t1.Elapsed += TimersTimeAction;
			t1.Start();
			Thread.Sleep(10000);
			t1.Stop();
			t1.Dispose();
		}

		public static void TimersTimeAction(object sender, System.Timers.ElapsedEventArgs e)
		{
			// TimeAction 需要满足 TimeAction 委托，返回类型是 void, 接收的参数类型是 Object
			LogHelper.Log($"System.Timers.Timer {DateTime.Now:T}");
		}
	}
}
