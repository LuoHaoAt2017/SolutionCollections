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

			t1.Join(); // �ȴ���ǰ�߳�ִ�����

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
			Thread.Sleep(500); // �߳�����������CPU��
			LogHelper.Log("product has expired");
		}
	}
}

/*
 * ���������������λ�ò���Thread.Yield()��Ӱ�쵽���򣬻�������ȷ������ bug��
 * ���ڵȴ�������״̬�����磬�ȴ������������û����룩���̲߳�����CPUʱ�䡣
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */