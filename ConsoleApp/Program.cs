
namespace ConsoleApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			Object obj = null;
			Console.WriteLine($"{obj == null}");
		}

		public static void TestStopwatch()
		{
			// Stopwatch 类
		}

		public static void TestForeach()
		{
			List<string> list = new List<string>() { "1", "2", "3", "4", "5", "6" };
			foreach (var item in list)
			{
				if (item == "3")
				{
					return;
				}
				Console.WriteLine(item);
			}
		}
	}
}