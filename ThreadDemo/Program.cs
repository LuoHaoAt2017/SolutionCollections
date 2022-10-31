using System;
using System.Collections.Generic;
using System.Linq;
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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmMain());
		}

		public static void TestParallel()
		{

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
	}
}
