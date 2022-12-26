
using System.Text;

public class Program
{
	private static int BUFFER_SIZE = 4096;

	public static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		ProcessASyncIO();
	}

	public static async void ProcessASyncIO()
	{
		using (var stream = new FileStream("test1.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None, BUFFER_SIZE))
		{
			Console.WriteLine("1: Uses I/O Threads: {0}", stream.IsAsync);
			byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());

			Task task = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
			await task;
		}

		using (var stream = new FileStream("test2.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None, BUFFER_SIZE, FileOptions.Asynchronous))
		{
			Console.WriteLine("2: Uses I/O Threads: {0}", stream.IsAsync);
			byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());

			Task task = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
			await task;
		}

		using (var stream = File.Create("test3.txt", BUFFER_SIZE, FileOptions.Asynchronous))
		{
			Console.WriteLine("3: Uses I/O Threads: {0}", stream.IsAsync);
			byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());

			Task task = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
			await task;
		}

		using (var stream = new StreamWriter("test4.txt", true))
		{
			Console.WriteLine("4: Uses I/O Threads: {0}", ((FileStream)stream.BaseStream).IsAsync);
			await stream.WriteAsync(CreateFileContent());
		}

		Task<long>[] readTasks = new Task<long>[4];
		for(int i = 0; i < 4; i++)
		{
			readTasks[i] = SumFileContent(string.Format("test{0}.txt", i+1));
		}
		long[] sums = await Task.WhenAll(readTasks);
		Console.WriteLine("sum in all file {0}", sums.Sum());
	}

	public static string CreateFileContent()
	{
		var sb = new StringBuilder();
		for(int i = 0; i < 10000; i++)
		{
			sb.AppendFormat("{0}", new Random(i).Next(0, 99999));
			sb.AppendLine();
		}
		return sb.ToString();
	}

	public static async Task<long> SumFileContent(string fileName)
	{
		using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None, BUFFER_SIZE, FileOptions.Asynchronous))
		{
			using (var sr = new StreamReader(stream))
			{
				long sum = 0;
				while(sr.Peek() > -1)
				{
					string line = await sr.ReadLineAsync();
					sum += long.Parse(line);
				}
				return sum;
			}
		}
	}
}