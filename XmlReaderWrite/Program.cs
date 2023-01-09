
using System.Xml;
using System.Xml.XPath;

public class Program
{
	public enum DefaultDB
	{
		MySql,
		SQLServer,
		Oracle,
		NULL
	}

	public static void Main(string[] args)
	{
		Console.WriteLine(GetDBType());
		Console.WriteLine(GetDBConn());
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
}
