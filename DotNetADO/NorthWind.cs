using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetADO.Models;
using DotNetADO.Helpers;

namespace DotNetADO
{
	public partial class NorthWind : Form
	{
		public ConfigurationInfo configuration;

		public DataTable dt = new DataTable("Customers");

		public NorthWind()
		{
			InitializeComponent();
		}

		public void NorthWindLoad(object sender, System.EventArgs e)
		{
			//LoadConfiguration();
			//SetupCustomerTable();
			//FillCustomerTable();
			//PrintCustomerTable();
			TestSqlSearch();
		}

		public void TestSqlCommand()
		{
			string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123;timeout=8";
			using (SqlConnection connection = new SqlConnection(source))
			{
				string sqlString = "SELECT * FROM Customers";
				SqlCommand cmd = new SqlCommand(sqlString, connection);
				cmd.CommandType = CommandType.Text;
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public void PrintCustomerTable()
		{
			foreach(DataRow row in dt.Rows)
			{
				LogHelper.Log($"{row[0].ToString()}-{row[1].ToString()}-{row[2].ToString()}-{row[3].ToString()}-{row[4].ToString()}-{row[5].ToString()}");
			}
		}

		public void FillCustomerTable()
		{
			var customers = Repository.GetCustomers(10000);
			foreach(Customer customer in customers)
			{
				DataRow row = dt.NewRow();
				row["customerId"] = customer.customerId;
				row["customerName"] = customer.customerName;
				row["contactName"] = customer.contactName;
				row["country"] = customer.country;
				row["city"] = customer.city;
				row["address"] = customer.address;
				row["postalCode"] = customer.postalCode;
				dt.Rows.Add(row);
			}
		}

		public void SetupCustomerTable()
		{
			// todo 反射技术
			
			if (configuration.customerColumn.customerName == "true")
			{
				dt.Columns.Add("CustomerName", Type.GetType("System.String"));
			}
			if (configuration.customerColumn.contactName == "true")
			{
				dt.Columns.Add("ContactName", Type.GetType("System.String"));
			}
			if (configuration.customerColumn.postalCode == "true")
			{
				dt.Columns.Add("PostalCode", Type.GetType("System.String"));
			}
			if (configuration.customerColumn.country == "true")
			{
				dt.Columns.Add("Country", Type.GetType("System.String"));
			}
			if (configuration.customerColumn.city == "true")
			{
				dt.Columns.Add("City", Type.GetType("System.String"));
			}
			if (configuration.customerColumn.address == "true")
			{
				dt.Columns.Add("Address", Type.GetType("System.String"));
			}
			// 设置主键
			dt.Columns.Add("CustomerId", Type.GetType("System.String"));
			DataColumn[] primaryKey = new DataColumn[1];
			primaryKey[0] = dt.Columns["CustomerId"];
			dt.PrimaryKey = primaryKey;
			//dt.Columns["CustomerId"].AutoIncrement = true;
			//dt.Columns["CustomerId"].AutoIncrementSeed = 1;
			//dt.Columns["CustomerId"].ReadOnly = true;
		}

		public void LoadConfiguration()
		{
			try
			{
				string filePath = AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";
				configuration = ConfigHelper.GetConfigInfo<ConfigurationInfo>(filePath);
				LogHelper.Log(configuration.customerColumn.customerName.ToString());
			}
			catch(Exception ex)
			{
				configuration = null;
				LogHelper.Error(ex.Message);
			}
		}

		public static void TestNorthWind()
		{
			try
			{
				string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123";
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
					TestWriteDatasetToXMLFile(conn);
					conn.Close();
					LogHelper.Log("数据库连接断开");
				}
			}
			catch (SqlException e)
			{
				LogHelper.Error(e.Message);
				throw new Exception(e.Message);
			}
		}

		public static void TestExecuteNonQueryCommand(SqlConnection conn)
		{
			// Yang Wang
			string sql = "UPDATE Customers SET PostalCode = @PostalCode WHERE ContactName = @ContactName";
			SqlCommand cmd = new SqlCommand(sql, conn);
			//cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar));
			//cmd.Parameters.Add(new SqlParameter("@ContactName", SqlDbType.VarChar));
			cmd.Parameters.AddWithValue("@PostalCode", "3021");
			cmd.Parameters.AddWithValue("@ContactName", "Yang Wang");
			// 一般用于 UPDATE, INSERT, DELETE 语句
			int rowCount = cmd.ExecuteNonQuery();
			LogHelper.Log($"{rowCount} rows returned");
		}

		public static void TestExecuteReaderCommand(SqlConnection conn)
		{
			string sql = "SELECT ContactName, CustomerName FROM Customers";
			SqlCommand cmd = new SqlCommand(sql, conn);
			SqlDataReader reader = cmd.ExecuteReader();
			DataTable table = reader.GetSchemaTable();
			foreach(DataRow row in table.Rows)
			{
				LogHelper.Log($"{row[0]}-{row[1]}-{row[2]}");
			}
			//while (reader.Read())
			//{
			//	LogHelper.Log($"Contact: {reader[0]} Company: {reader[1]}");
			//}
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
			foreach (DataRow row in ds.Tables["Customers"].Rows)
			{
				foreach (DataColumn col in ds.Tables["Customers"].Columns)
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

		public async static Task<int> GetCustomerCount()
		{
			using (SqlConnection conn = (SqlConnection)DBHelper.GetDBConnection("Northwind"))
			{
				conn.Open();
				string sql = "WAITFOR DELAY '0:0:02'; SELECT COUNT(*) FROM Customers";
				SqlCommand cmd = new SqlCommand(sql, conn);
				return await cmd.ExecuteScalarAsync().ContinueWith(t => Convert.ToInt32(t.Result));
			}
		}

		public async static Task<int> GetEmployeeCount()
		{
			using (SqlConnection conn = (SqlConnection)DBHelper.GetDBConnection("Northwind"))
			{
				conn.Open();
				string sql = "WAITFOR DELAY '0:0:03'; SELECT COUNT(*) FROM Employees";
				SqlCommand cmd = new SqlCommand(sql, conn);
				return await cmd.ExecuteScalarAsync().ContinueWith(t => Convert.ToInt32(t.Result));
			}
		}


		// SQL 的增删改查
		public static void TestSqlInsert()
		{
			string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123;";
			using (SqlConnection connection = new SqlConnection(source))
			{
				SqlCommand command = connection.CreateCommand();
				string commandText = "INSERT INTO Categories(CategoryName,Description) VALUES(@CategoryName,@Description)";
				command.CommandText = commandText;
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@CategoryName", "Mushroom");
				command.Parameters.AddWithValue("@Description", "菌类");
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public static void TestSqlDelete()
		{
			string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123;";
			using (SqlConnection connection = new SqlConnection(source))
			{
				string commandText = "DELETE FROM Categories WHERE CategoryID=@CategoryID";

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText = commandText;
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@CategoryID", "1002");
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public static void TestSqlUpdate()
		{
			string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123;";
			using (SqlConnection connection = new SqlConnection(source))
			{
				SqlCommand command = connection.CreateCommand();
				string commandText = "UPDATE Categories SET CategoryName=@CategoryName, Description=@Description WHERE CategoryID=@CategoryID";
				command.CommandText = commandText;
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@CategoryID", "1");
				command.Parameters.AddWithValue("@CategoryName", "Beverages");
				command.Parameters.AddWithValue("@Description", "Soft drinks, coffees, teas, beers, and ales");
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public static void TestSqlSelect()
		{
			string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123;";
			using (SqlConnection connection = new SqlConnection(source))
			{
				SqlCommand cmd = connection.CreateCommand();
				cmd.Connection = connection;
				cmd.CommandText = "SELECT * FROM Categories";
				cmd.CommandType = CommandType.Text;
				connection.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				DataTable table = reader.GetSchemaTable();
				foreach(DataRow row in table.Rows)
				{
					LogHelper.Log($"{row[0]}-{row[1]}-{row[2]}");
				}
				connection.Close();
			}
		}

		public static void TestSqlSearch()
		{
			string source = "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123;";
			using (SqlConnection connection = new SqlConnection(source))
			{
				SqlCommand command  = connection.CreateCommand();
				command.Connection = connection;
				command.CommandText = "SELECT * FROM Categories";
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@CategoryName", "Tuber");
				command.Parameters.AddWithValue("@Description", "块茎");
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				DataSet ds = new DataSet();
				adapter.Fill(ds, "Categories");
				foreach(DataRow row in ds.Tables["Categories"].Rows)
				{
					LogHelper.Log($"CategoryID{row[0]} CategoryName {row[1]} Description {row[2]}");
				}
			}
		}
	}
}
