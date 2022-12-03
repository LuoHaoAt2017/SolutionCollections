namespace Multithreading
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
			Test2();
		}

		public void Test1()
		{
			Product product = new Product();

			new Thread(product.Sale).Start();

			product.Sale();


		}

		public void Test2()
		{
			Product product = new Product();

			Thread t1 = new Thread(product.Expire);

			t1.Start();

			t1.Join(); // 等待当前线程执行完毕

			LogHelper.Log("product status");
		}
	}

	public class Product
	{
		private bool isSaled = false;

		private readonly object locker = new object();

		public void Sale()
		{
			lock(locker)
			{
				if (!this.isSaled)
				{
					LogHelper.Log("product has saled");
					this.isSaled = true;
				}
			}
		}

		public void Expire()
		{
			Thread.Sleep(500); // 线程阻塞，交出CPU。
			LogHelper.Log("product has expired");
		}
	}
}

/*
 * 如果在你代码的任意位置插入Thread.Yield()会影响到程序，基本可以确定存在 bug。
 * 处于等待或阻塞状态（例如，等待排它锁或者用户输入）的线程不消耗CPU时间。
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */