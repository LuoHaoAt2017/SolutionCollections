﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetADO.Helpers
{
	internal class DBHelper
	{
		public DbConnection GetDBConnection(string name)
		{
			// 读取 App.config 中的 Northwind 配置项
			ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
			// 根据 System.Data.SqlClient 创建工厂
			DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
			// 根据工厂函数创建 connection 连接
			DbConnection connection = factory.CreateConnection();
			// server=WH1301000467;integrated security=SSPI;database=Northwind
			connection.ConnectionString = settings.ConnectionString;
			return connection;
		}
	}
}