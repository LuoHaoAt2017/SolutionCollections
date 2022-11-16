using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTask
{
	public partial class FrmMain : Form
	{
		private static CancellationTokenSource tokenSource = new CancellationTokenSource();

		private static HttpClient client = new HttpClient
		{
			MaxResponseContentBufferSize = 1_000_000
		};

		private static Stopwatch watch = new Stopwatch();

		public FrmMain()
		{
			InitializeComponent();
			// CombinateAsync();
			// TestCancelRequest();
			TestSyncTask();
		}

		public static void TestSyncTask()
		{
			const int LargeNumber = 90000000;
			watch.Start();
			RunCpuConsumingTask(1, LargeNumber);
			RunCpuConsumingTask(2, LargeNumber);
			RunCpuConsumingTask(3, LargeNumber);
			RunCpuConsumingTask(4, LargeNumber);
		}

		public static void RunCpuConsumingTask(int id, int LargeNumber)
		{
			for (int i = 0; i < LargeNumber; i++) ;
			LogHelper.Log($"{id} count: {watch.Elapsed.TotalMilliseconds} ms");
		}

		public static void GetHttpResponse(int id, string url)
		{
			WebClient client = new WebClient();
			LogHelper.Log($"start {id} task. count: {watch.Elapsed.TotalMilliseconds} ms");
			client.DownloadString(new Uri(url));
		}

		// 无法获取异常
		public static void DontHandle()
		{
			try
			{
				ThrowAfter(200, "first");
			}
			catch (Exception ex)
			{
				// 错误并不会被捕获
				LogHelper.Error(ex.Message);
			}
			finally
			{
				Console.WriteLine("try...catch...finally 执行完毕时，ThrowAfter 还没有抛出异常");
			}
		}

		// 取消运行很长时间的任务
		public static async Task ThrowAfter(int ms, string message)
		{
			await Task.Delay(ms);
			throw new Exception(message);
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

		public static string Greeting(string name)
		{
			Thread.Sleep(1000);
			return name;
		}

		public static Task<bool> CongratulateAsync(bool value)
		{
			return Task.Run<bool>(() =>
			{
				Thread.Sleep(1500);
				return value;
			});
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

		public static async Task TestCancelRequest()
		{
			IEnumerable<string> urlList = new string[]
			{
				"https://docs.microsoft.com",
				"https://docs.microsoft.com/aspnet/core",
				"https://docs.microsoft.com/azure",
				"https://docs.microsoft.com/azure/devops",
				"https://docs.microsoft.com/dotnet",
				"https://docs.microsoft.com/dynamics365",
				"https://docs.microsoft.com/education",
				"https://docs.microsoft.com/enterprise-mobility-security",
				"https://docs.microsoft.com/gaming",
				"https://docs.microsoft.com/graph",
				"https://docs.microsoft.com/microsoft-365",
				"https://docs.microsoft.com/office",
				"https://docs.microsoft.com/powershell",
				"https://docs.microsoft.com/sql",
				"https://docs.microsoft.com/surface",
				"https://docs.microsoft.com/system-center",
				"https://docs.microsoft.com/visualstudio",
				"https://docs.microsoft.com/windows",
				"https://docs.microsoft.com/xamarin"
			};
			LogHelper.Log("Application started.");

			try
			{
				tokenSource.CancelAfter(3500);
				await SumPageSizeAsync(urlList, tokenSource.Token);
			}
			catch(OperationCanceledException)
			{
				LogHelper.Error("\nTasks cancelled: timed out.\n");
			}
			finally
			{
				tokenSource.Dispose();
			}

			LogHelper.Log("Application ending.");
		}

		public static async Task SumPageSizeAsync(IEnumerable<string> urlList, CancellationToken token)
		{
			var stopWatch = Stopwatch.StartNew();
			foreach(string url in urlList)
			{
				await ProcessUrlAsync(url, token);
			}
			stopWatch.Stop();
			LogHelper.Log($"Elapsed time:{stopWatch.Elapsed}\n");
		}

		public static async Task ProcessUrlAsync(string url, CancellationToken token)
		{
			HttpResponseMessage response = await client.GetAsync(url, token);
			byte[] content = await response.Content.ReadAsByteArrayAsync();
			LogHelper.Log($"{url,-60} {content.Length, 10:#,#}");
		}
	}
}
