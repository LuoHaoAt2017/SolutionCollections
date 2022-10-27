using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace DotNetADO
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
			TestNorthWind();
			Application.Run(new MainForm());
		}

		public static void TestNorthWind()
		{
			// Server=localhost;Database=master;Trusted_Connection=True;
			// string source = "server=(local);" + "integrated security=SSPI;" + "database=NorthWind";
			//string source = "Server=WH1301000467;Database=master;Trusted_Connection=True";
			
			try
			{
				string source = "Server=WH1301000467;Database=Northwind;Trusted_Connection=True";
				using (SqlConnection conn = new SqlConnection(source))
				{
					conn.Open();
					LogHelper.Log("数据库连接成功");
					TestDataSet(conn);
					conn.Close();
					LogHelper.Log("数据库连接断开");
				}
			}
			catch(SqlException e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void TestDataSet(SqlConnection conn)
		{
			// DataSet 类基本上是内存中的数据库，包含了所有表，关系，约束。
			// DataSet 和相关的类已经被 Entity Framework 代替。
			LogHelper.Log("数据库查询");
			string sql = "SELECT ContactName,CompanyName FROM Customers";
			SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
			DataSet ds = new DataSet();
			adapter.Fill(ds, "Customers");
			TestDataRow(ds);
		}

		public static void TestDataRow(DataSet ds)
		{
			foreach (DataRow row in ds.Tables["Customers"].Rows)
			{
				LogHelper.Log($"{row[0]} from {row[1]}");
			}
			foreach(DataRow row in ds.Tables["Customers"].Rows)
			{
				foreach(DataColumn col in ds.Tables["Customers"].Columns)
				{
					LogHelper.Log($"{col.ColumnName} Current = {row[col, DataRowVersion.Current]}");
					LogHelper.Log($"{col.ColumnName} Default = {row[col, DataRowVersion.Default]}");
					LogHelper.Log($"{col.ColumnName} Original = {row[col, DataRowVersion.Original]}");
				}
			}
		}
	}
}
