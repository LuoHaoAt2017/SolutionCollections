
using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;

public class Program
{
	public static void Main(string[] args)
	{
		Test4();
	}

	/// <summary>
	/// 查询属性节点
	/// </summary>
	public static void Test1()
	{
		string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";
		using (FileStream stream = File.OpenRead(filePath))
		{
			// XPathDocument 的唯一功能是创建 XPathNavigator。
			XPathDocument xPathDocument = new XPathDocument(stream);
			// XPathNavigator 包含移动和选择元素的方法。
			// 可以移动到一个具体的元素，也可以移动到移动到元素的某个属性。
			XPathNavigator navigator = xPathDocument.CreateNavigator();
			var node = navigator.SelectSingleNode("/Root/DBType");
			if (node != null)
			{
				if(node.MoveToAttribute("Default", ""))
				{
					Console.WriteLine(node.Name + " : " +  node.Value);
				}
			}
		}
	}

	/// <summary>
	/// 遍历后代节点
	/// </summary>
	public static void Test2()
	{
		string path = System.AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";
		var document = new XPathDocument(path);
		XPathNavigator navigator = document.CreateNavigator();
		XPathNodeIterator iterator = navigator.Select("/Root/DB");
		while(iterator != null && iterator.MoveNext())
		{
			if (iterator.Current != null)
			{
				XPathNodeIterator children = iterator.Current.SelectDescendants(type: XPathNodeType.Element, matchSelf: false);
				while (children.MoveNext())
				{
					if (children.Current != null)
					{
						string serverIp = children.Current.GetAttribute("Server", String.Empty);
						string username = children.Current.GetAttribute("DBUser", String.Empty);
						string password = children.Current.GetAttribute("DBPwd", String.Empty);
						string database = children.Current.GetAttribute("DBName", String.Empty);
						Console.WriteLine($"Name={children.Current.Name}, Server={serverIp},DBUser={username},DBPwd={password},DBName={database}");
					}
				}
			}
		}
	}

	/// <summary>
	/// 综合运用
	/// </summary>
	public static void Test3()
	{
		Console.WriteLine(GetDBType());
		Console.WriteLine(GetDBConn());
	}

	public enum DefaultDB
	{
		MySql,
		SQLServer,
		Oracle,
		NULL
	}

	/// <summary>
	/// 读取数据库类型
	/// </summary>
	public static DefaultDB GetDBType()
	{
		string dbType = GetElemAttr("/Root/DBType", "Default");
		if (dbType == DefaultDB.MySql.ToString())
		{
			return DefaultDB.MySql;
		}
		if (dbType == DefaultDB.SQLServer.ToString())
		{
			return DefaultDB.SQLServer;
		}
		if (dbType == DefaultDB.Oracle.ToString())
		{
			return DefaultDB.Oracle;
		}
		return DefaultDB.NULL;
	}

	/// <summary>
	/// 读取数据库连接
	/// </summary>
	public static string GetDBConn()
	{
		DefaultDB dbType = GetDBType();
		if (dbType == DefaultDB.MySql)
		{
			return GetDBServer("/Root/DB/MySql");
		}
		if (dbType == DefaultDB.SQLServer)
		{
			return GetDBServer("/Root/DB/SQLServer");
		}
		if (dbType == DefaultDB.Oracle)
		{
			return GetDBServer("/Root/DB/Oracle");
		}
		return "";
	}

	private static string GetDBServer(string dbType)
	{
		string serverIp = GetElemAttr(dbType, "Server");
		string username = GetElemAttr(dbType, "DBUser");
		string password = GetElemAttr(dbType, "DBPwd");
		string database = GetElemAttr(dbType, "DBName");
		return $"server={serverIp};User Id={username};password={password};database={database}";
	}

	private static string GetElemAttr(string xmlPath, string attrName)
	{
		string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";

		if (!File.Exists(filePath))
		{
			return "";
		}

		using (FileStream stream = File.OpenRead(filePath))
		{
			XPathDocument doc = new XPathDocument(stream);
			XPathNavigator navigator = doc.CreateNavigator();
			XPathNodeIterator iterator = navigator.Select(xmlPath);
			if (iterator == null || iterator.Current == null)
			{
				return "";
			}
			while (iterator.MoveNext())
			{
				string value = iterator.Current.GetAttribute(attrName, "");
				if (!string.IsNullOrEmpty(value))
				{
					return value;
				}
			}
			return "";
		}
	}

	public class Book
	{
		public Book(string title, string price)
		{
			this.Title = title;
			this.Price = price;
		}

		public string Title { get; set; }

		public string Price { get; set; }
	}
}
