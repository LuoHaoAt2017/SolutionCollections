using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

// DataSet 类基本上是内存中的数据库，包含了所有表，关系，约束。
// DataSet 和相关的类已经被 Entity Framework 代替。

// SelectCommand
// InsertCommand
// DeleteCommand
// UpdateCommand

// 对频繁使用的命令采用存储过程来执行
// 对不常用的命令直接用SQL命令来执行

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
					// TestSelectStoredProcedure(conn);
					// TestInsertStoredProcedure(conn);
					// TestUpdateStoredProcedure(conn);
					// TestDeleteStoredProcedure(conn);
					// TestWriteDatasetToXMLFile(conn);
					// TestExecuteNonQueryCommand(conn);
					// TestExecuteReaderCommand(conn);
					// TestExecuteScalarCommand(conn);
					conn.Close();
					LogHelper.Log("数据库连接断开");
				}
			}
			catch(SqlException e)
			{
				LogHelper.Error(e.Message);
				throw new Exception(e.Message);
			}
		}

		public static void TestExecuteNonQueryCommand(SqlConnection conn)
		{
			string sql = "UPDATE Customers SET ContactName = 'Bob' WHERE ContactName = 'Bill'";
			SqlCommand cmd = new SqlCommand(sql, conn);
			// 一般用于 UPDATE, INSERT, DELETE 语句
			int rowCount =  cmd.ExecuteNonQuery();
			LogHelper.Log($"{rowCount} rows returned");
		}

		public static void TestExecuteReaderCommand(SqlConnection conn)
		{
			string sql = "SELECT ContactName, CompanyName FROM Customers";
			SqlCommand cmd = new SqlCommand(sql, conn);
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				LogHelper.Log($"Contact: {reader[0]} Company: {reader[1]}");
			}
		}

		public static void TestExecuteScalarCommand(SqlConnection conn)
		{
			string sql = "SELECT COUNT(*) FROM Customers";
			SqlCommand cmd = new SqlCommand(sql, conn);
			object obj = cmd.ExecuteScalar();
			Console.WriteLine(obj); // 返回表中的记录个数
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

		public static void TestSelectStoredProcedure(SqlConnection conn)
		{
			//CREATE PROCEDURE RegionSelect AS
			//	SET NOCOUNT OFF
			//	SELECT* FROM Region
			//GO
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

		public static void TestInsertStoredProcedure(SqlConnection conn)
		{
			//CREATE PROCEDURE RegionInsert(@RegionID INTEGER, @RegionDescription NCHAR(50))
			//	AS SET NOCOUNT OFF
			//	SELECT @RegionID = MAX(RegionID) + 1 FROM Region
			//	INSERT INTO Region(RegionID, RegionDescription)
			//	VALUES(@RegionID, @RegionDescription)
			//GO
			SqlCommand cmd = new SqlCommand("RegionInsert", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@RegionDescription", SqlDbType.NChar, 50, "RegionDescription"));
			cmd.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int, 5, "RegionID"));
			cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
			cmd.Parameters["@RegionDescription"].Value = "South East";
			cmd.Parameters["@RegionID"].Value = 5;
			cmd.ExecuteNonQuery();
		}

		public static void TestUpdateStoredProcedure(SqlConnection conn)
		{
			//CREATE PROCEDURE RegionUpdate(@RegionID INTEGER, @RegionDescription NCHAR(50))
			//	AS SET NOCOUNT OFF
			//	UPDATE Region
			//	SET RegionDescription = @RegionDescription WHERE RegionID = @RegionID
			//GO
			SqlCommand cmd = new SqlCommand("RegionUpdate", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@RegionID", 7);
			cmd.Parameters.AddWithValue("@RegionDescription", "WestNorth");
			cmd.ExecuteNonQuery();
		}

		public static void TestDeleteStoredProcedure(SqlConnection conn)
		{
			//CREATE PROCEDURE RegionDelete(@RegionID INTEGER)
			//	AS SET NOCOUNT OFF
			//	DELETE FROM Region WHERE RegionID = @RegionID
			//GO
			SqlCommand cmd = new SqlCommand("RegionDelete", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.UpdatedRowSource = UpdateRowSource.None;
			cmd.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int, 0, "RegionID"));
			cmd.Parameters["@RegionID"].Value = 6;
			cmd.ExecuteNonQuery();
		}

		public static void TestWriteDatasetToXMLFile(SqlConnection conn)
		{
			SqlCommand cmd = new SqlCommand("RegionSelect", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.UpdatedRowSource = UpdateRowSource.None;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = cmd;
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "Region");
			dataSet.WriteXml(".\\WithoutSchema.xml", XmlWriteMode.IgnoreSchema);
			dataSet.WriteXml(".\\WithSchema.xml", XmlWriteMode.WriteSchema);
		}
	}
}
