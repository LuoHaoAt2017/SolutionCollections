using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTask
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
			Application.Run(new Entry());
			CallWithAsync();
		}

		private static async void CallWithAsync()
		{
			try
			{
				string res1 = await GreetingAsync("Hello World");
				Console.WriteLine("res1: {0}", res1);

				string res2 = await GreetingAsync("Hello Darkness");
				Console.WriteLine("res2: {0}", res2);
			}
			catch (Exception ex)
			{
				Console.WriteLine("mesg: {0}", ex.Message);
			}
		}

		public static void CallWithTask()
		{
			Task<string> t1 = GreetingAsync("YYYY");
			t1.ContinueWith(t =>
			{
				Console.WriteLine(t.Result);
			});
		}

		public static string Greeting(string name)
		{
			Thread.Sleep(1000);
			return name;
		}

		public static Task<string> GreetingAsync(string name)
		{
			return Task.Run<string>(() =>
			{
				return Greeting(name);
			});
		}

		public static async Task Method1()
		{
			await Task.Run(() =>
			{
				for (int i = 0; i < 100; i++)
				{
					Console.WriteLine("Method 1");
				}
			});
		}

		public static void Method2()
		{
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine("Method 2");
			}
		}
	}
}
