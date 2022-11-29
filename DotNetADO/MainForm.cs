using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace DotNetADO
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		// 隔离级别衡量事务外的改变对事务的影响程度。
		// 隔离级别越高，事务的独立性越高，数据库的并发性越低。
		private void MainFormLoad(object sender, System.EventArgs e)
		{
			//new Thread(() =>
			//{
			//	TestIsolationLevel1();
			//}).Start();

			//new Thread(() =>
			//{
			//	TestIsolationLevel2();
			//}).Start();

			//new Thread(() =>
			//{
			//	TestIsolationLevel3();
			//	TestIsolationLevel3();
			//}).Start();

			// TestIsolationLevel4();
			// TestIsolationLevel3();
			// TestIsolationLevel3();
		}

		public void TestIsolationLevel1()
		{
			string source = "server=10.5.67.45;database=Northwind;uid=tomcat;pwd=LuoHao123;";
			SqlConnection connection = new SqlConnection(source);
			connection.Open();
			SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Transaction = transaction;
				cmd.Connection = connection;
				cmd.CommandText = "INSERT INTO Region(RegionID,RegionDescription) VALUES(8,'EastNorth')";
				cmd.ExecuteNonQuery();
				Thread.Sleep(1000);
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex.Message);
				transaction.Rollback();
			}
			finally
			{
				connection.Close();
			}
		}

		public void TestIsolationLevel2()
		{
			string source = "server=10.5.67.45;database=Northwind;uid=tomcat;pwd=LuoHao123;";
			SqlConnection connection = new SqlConnection(source);
			connection.Open();
			SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Transaction = transaction;
				cmd.Connection = connection;
				cmd.CommandText = "SELECT TOP 1 * FROM Region ORDER BY RegionID DESC";
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					LogHelper.Log($"{reader.GetInt32(0)}-{reader.GetString(1)}");
				}
				reader.Close();
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex.Message);
				transaction.Rollback();
			}
			finally
			{
				connection.Close();
			}
		}

		public void TestIsolationLevel3()
		{
			string source = "server=10.5.67.45;database=Northwind;uid=tomcat;pwd=LuoHao123;";
			SqlConnection connection = new SqlConnection(source);
			connection.Open();
			SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Transaction = transaction;
				cmd.Connection = connection;
				cmd.CommandText = "DELETE FROM Region WHERE RegionID = 6";
				int rows = cmd.ExecuteNonQuery();
				transaction.Commit();
				LogHelper.Log($"删除成功条数：{rows}");
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex.Message);
				transaction.Rollback();
			}
			finally
			{
				connection.Close();
			}
		}

		public void TestIsolationLevel4()
		{
			string source = "server=10.5.67.45;database=Northwind;uid=tomcat;pwd=LuoHao123;";
			SqlConnection connection = new SqlConnection(source);
			connection.Open();
			SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Transaction = transaction;
				cmd.Connection = connection;
				cmd.CommandText = "INSERT INTO Region(RegionID,RegionDescription) VALUES(6,'EastNorth')";
				int rows = cmd.ExecuteNonQuery();
				transaction.Commit();
				LogHelper.Log($"新增成功条数：{rows}");
			}
			catch (Exception ex)
			{
				LogHelper.Error(ex.Message);
				transaction.Rollback();
			}
			finally
			{
				connection.Close();
			}
		}
	}
}
