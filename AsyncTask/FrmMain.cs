using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTask
{
	public partial class FrmMain : Form
	{
		private CancellationTokenSource tokenSource = new CancellationTokenSource();
		public FrmMain()
		{
			InitializeComponent();
			// CombinateAsync();
			HandleError4();
		}

		// 取消运行很长时间的任务


		public static async Task ThrowAfter(int ms, string message)
		{
			await Task.Delay(ms);
			throw new Exception(message);
		}
		// 无法获取异常
		public static void DontHandle()
		{
			try
			{
				ThrowAfter(200, "first");
			}
			catch(Exception ex)
			{
				// 错误并不会被捕获
				LogHelper.Error(ex.Message);
			}
			finally
			{
				Console.WriteLine("try...catch...finally 执行完毕时，ThrowAfter 还没有抛出异常");
			}
		}
		// 获取部分异常
		public static async void HandleError1()
		{
			try
			{
				await ThrowAfter(200, "Error1");
				await ThrowAfter(100, "Error2");
			}
			catch (Exception ex)
			{
				// 错误会被捕获
				LogHelper.Error(ex.Message);
			}
		}
		// 获取部分异常
		public static async void HandleError2()
		{
			try
			{
				Task t1 = ThrowAfter(200, "Error1");
				Task t2 = ThrowAfter(100, "Error2");
				await Task.WhenAll(t1, t2);
			}
			catch (Exception ex)
			{
				// 错误会被捕获
				LogHelper.Error(ex.Message);
			}
		}
		// 获取所有异常
		public static async void HandleError3()
		{
			Task t1 = null;
			Task t2 = null;
			try
			{
				t1 = ThrowAfter(200, "Error1");
				t2 = ThrowAfter(100, "Error2");
				await Task.WhenAll(t1, t2);
			}
			catch (Exception)
			{
				// 错误会被捕获
				LogHelper.Error($"t1: {t1.IsFaulted} {t1.Exception.Message}, t2: {t2.IsFaulted} {t2.Exception.Message}");
			}
		}
		// 获取所有异常
		public static async void HandleError4()
		{
			Task task = null;
			try
			{
				Task t1 = ThrowAfter(200, "Error1");
				Task t2 = ThrowAfter(100, "Error2");
				await (task = Task.WhenAll(t1, t2));
			}
			catch
			{
				foreach(var ex in task.Exception.InnerExceptions)
				{
					LogHelper.Error($"exceptions: {ex.Message}");
				}
			}
		}

		public async static void CombinateAsync()
		{
			Task<string> t1 = GreetingAsync("Hello1");
			Task<string> t2 = GreetingAsync("Hello2");
			await Task.WhenAll(t1, t2);
			Console.WriteLine($"async-1: {t1.Result}, async-2: {t2.Result}");
			LogHelper.Log($"async-1: {t1.Result}, async-2: {t2.Result}");

			Task<string> t3 = GreetingAsync("Hello3");
			Task<bool> t4 = CongratulateAsync(true);
			await Task.WhenAll(t3, t4);
			LogHelper.Log($"async-3: {t3.Result}, async-4: {t4.Result}");
		}

		public static async void CallWithAsync()
		{
			try
			{
				// 按顺序调用异步方法
				string res1 = await GreetingAsync("Hello World");
				LogHelper.Log($"res1: {res1}");
				string res2 = await GreetingAsync("Hello Darkness");
				LogHelper.Log($"res2: {res2}");
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex.Message);
			}
		}

		public static Task<string> GreetingAsync(string name)
		{
			// 返回一个字符串任务
			return Task.Run<string>(() =>
			{
				return Greeting(name);
			});
		}

		public static Task<bool> CongratulateAsync(bool value)
		{
			return Task.Run<bool>(() =>
			{
				Thread.Sleep(1500);
				return value;
			});
		}

		public static string Greeting(string name)
		{
			Thread.Sleep(1000);
			return name;
		}

		public static void CallWithContinuationTask()
		{
			Task<string> t1 = GreetingAsync("YYYY");
			// 类似于 Promise.then
			t1.ContinueWith(t =>
			{
				LogHelper.Log(t.Result);
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
