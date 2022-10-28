using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

// DataSet 类基本上是内存中的数据库，包含了所有表，关系，约束。
// DataSet 和相关的类已经被 Entity Framework 代替。

namespace DotNetADO
{
	internal static class Program
	{
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
			try
			{
				string source = "Server=WH1301000467;Database=Northwind;Trusted_Connection=True";
				using (SqlConnection conn = new SqlConnection(source))
				{
					conn.Open();
					LogHelper.Log("数据库连接成功");
					TestStoredProcedure(conn);
					conn.Close();
					LogHelper.Log("数据库连接断开");
				}
			}
			catch(SqlException e)
			{
				throw new Exception(e.Message);
			}
		}

		public static void ManufactureCustomerDataTable(SqlConnection conn)
		{
			DataSet ds = new DataSet();
			string sql = "SELECT ContactName,CompanyName FROM Customers";
			SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
			adapter.Fill(ds, "Customers");
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

		public static void ManufactureProductDataTable(SqlConnection conn)
		{
			DataSet ds = new DataSet();
			string sql = "SELECT * FROM Products";
			SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
			DataTable dataTable = new DataTable("Products");
			dataTable.Columns.Add(new DataColumn("ProductID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("ProductName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("SupplierId", typeof(int)));
			dataTable.Columns.Add(new DataColumn("CategoryId", typeof(int)));
			dataTable.Columns.Add(new DataColumn("QuantityPerUnit", typeof(string)));
			dataTable.Columns.Add(new DataColumn("UnitPrice", typeof(decimal)));
			dataTable.Columns.Add(new DataColumn("UnitsInStock", typeof(short)));
			dataTable.Columns.Add(new DataColumn("UnitsOnOrder", typeof(short)));
			dataTable.Columns.Add(new DataColumn("ReorderLevel", typeof(short)));
			dataTable.Columns.Add(new DataColumn("Discontinued", typeof(bool)));
			adapter.Fill(ds, "Products");
			foreach (DataRow row in ds.Tables["Products"].Rows)
			{
				LogHelper.Log($"{row[0]} from {row[1]}");
			}
		}

		public static void TestRelationshipBetweenTable()
		{
			// 如何手工生成并充填两个数据库
			DataSet ds = new DataSet("Relationships");

			DataTable dataTable1 = new DataTable("Building");
			dataTable1.Columns.Add("BuildingID", typeof(int));
			dataTable1.Columns.Add("Name", typeof(string));
			DataColumn[] pk1 = new DataColumn[1]; // 主键可以是一列或者多列
			pk1[0] = dataTable1.Columns["BuildingID"];
			dataTable1.Constraints.Add(new UniqueConstraint("PK_Building", pk1[0]));
			dataTable1.PrimaryKey = pk1;

			DataTable dataTable2 = new DataTable("Room");
			dataTable2.Columns.Add(new DataColumn("RoomID", typeof(int)));
			dataTable2.Columns.Add(new DataColumn("Name", typeof(string)));
			dataTable2.Columns.Add(new DataColumn("BuildingID", typeof(int)));
			DataColumn[] pk2 = new DataColumn[1]; // 主键可以是一列或者多列
			pk2[0] = dataTable2.Columns["RoomID"];
			dataTable2.Constraints.Add(new UniqueConstraint("PK_Room", pk2[0]));
			dataTable2.PrimaryKey = pk2;

			ds.Tables.Add(dataTable1);
			ds.Tables.Add(dataTable2);

			DataColumn col1 = ds.Tables["Building"].Columns["BuildingID"];
			DataColumn col2 = ds.Tables["Room"].Columns["BuildingID"];
			ds.Relations.Add("Rooms", col1, col2);
			//// 从父记录中寻找子记录
			foreach (DataRow building in ds.Tables["Building"].Rows)
			{
				DataRow[] rooms = building.GetChildRows("Rooms"); // 每一栋建筑有多个房间
				LogHelper.Log($"Building {building["Name"]} contains {rooms.Length} room");
				foreach (DataRow room in rooms)
				{
					LogHelper.Log($"Room: {room["Name"]}");
				}
			}
			//// 从子记录中寻找父记录
			foreach (DataRow room in ds.Tables["Room"].Rows)
			{
				DataRow[] buildings = room.GetParentRows("Rooms");
				foreach (DataRow building in buildings)
				{
					LogHelper.Log($"Room {room["Name"]} is contained in building {building["Name"]}");
				}
			}
		}

		public static void TestForeignKeyConstraint()
		{
			DataSet ds = new DataSet();

			DataTable products = new DataTable("Products");
			ds.Tables.Add(products);
			products.Columns.Add(new DataColumn("ProductID", typeof(int)));
			products.Columns.Add(new DataColumn("ProductName", typeof(string)));
			products.Columns.Add(new DataColumn("SupplierId", typeof(int)));
			products.Columns.Add(new DataColumn("CategoryId", typeof(int)));
			products.Columns.Add(new DataColumn("QuantityPerUnit", typeof(string)));
			products.Columns.Add(new DataColumn("UnitPrice", typeof(decimal)));
			products.Columns.Add(new DataColumn("UnitsInStock", typeof(short)));
			products.Columns.Add(new DataColumn("UnitsOnOrder", typeof(short)));
			products.Columns.Add(new DataColumn("ReorderLevel", typeof(short)));
			products.Columns.Add(new DataColumn("Discontinued", typeof(bool)));

			DataTable categories = new DataTable("Categories");
			ds.Tables.Add(categories);
			categories.Columns.Add(new DataColumn("CategoryID", typeof(int)));
			categories.Columns.Add(new DataColumn("CategoryName", typeof(string)));
			categories.Columns.Add(new DataColumn("Description", typeof(string)));
			categories.Constraints.Add(new UniqueConstraint("PK_Categories", categories.Columns["CategoryID"]));
			categories.PrimaryKey = new DataColumn[1] { categories.Columns["CategoryID"] };

			DataColumn parent = products.Columns["ProductID"];
			DataColumn child = categories.Columns["CategoryID"];
			ForeignKeyConstraint fk = new ForeignKeyConstraint("FK_Product_CategoryID", parent, child);
			fk.UpdateRule = Rule.Cascade;
			fk.DeleteRule = Rule.SetNull;
			products.Constraints.Add(fk);
		}

		public static void TestStoredProcedure(SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand("RegionSelect", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.UpdatedRowSource = UpdateRowSource.None;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = cmd;
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "Region"); // 执行语句
			foreach (DataRow row in dataSet.Tables["Region"].Rows)
			{
				LogHelper.Log($"{row[0]} from {row[1]}");
			}
		}
	}
}
