using System.Configuration;
using System.Data.Common;

namespace DotNetADO.Helpers
{
	internal class DBHelper
	{
		public static DbConnection GetDBConnection(string name)
		{
			// 读取 App.config 中的 Northwind 配置项
			ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
			// 根据 System.Data.SqlClient 创建工厂
			DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
			// 根据工厂函数创建 connection 连接
			DbConnection connection = factory.CreateConnection();
			// "server=192.168.0.193;database=northwind;uid=tomcat;pwd=LuoHao123";
			connection.ConnectionString = settings.ConnectionString;
			return connection;
		}
	}
}
