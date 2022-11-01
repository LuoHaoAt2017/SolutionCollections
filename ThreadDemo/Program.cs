using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadDemo
{
	internal static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Application.EnableVisualStyles();
			// Application.SetCompatibleTextRenderingDefault(false);
			// Application.Run(new FrmMain());
			TestMainThread();
		}

		public static void MainTread()
		{
			LogHelper.Log($"Thread {Thread.CurrentThread.Name} started");
			Thread.Sleep(3000);
			LogHelper.Log($"Thread {Thread.CurrentThread.Name} completed");
		}

		public static void TestMainThread()
		{
			var t1 = new Thread(MainTread)
			{
				// 只要有一个前台线程在运行，应用程序的进程就在运行。
				// 如果多个前台线程在运行，但是Main方法结束了，应用程序的进程仍然是激活的，直到所有的前台线程完成其任务。
				Name = "MainTread1",
				IsBackground = true
			};
			t1.Start();
			LogHelper.Log("Main thread ending now");
		}
	}
}
