using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DotNetADO.Helpers
{
	class ConfigHelper
	{
		public static T GetConfigInfo<T>(string xmlPath) where T : class
		{
			T configInfo = default(T); // T t=default(T),就是初始化，值类型的话，就是T t=0;引用类型的话，就是T t=null
			try
			{
				object obj = Deserialize(typeof(T), xmlPath);
				if (obj != null && obj is T)
				{
					configInfo = (T)obj;
				}
			}
			catch (Exception)
			{
				LogHelper.Log("GetConfigInfo获取配置信息失败");
			}
			return configInfo;
		}

		public static object Deserialize(Type type, string xmlPath)
		{
			object result = null;
			try
			{
				using (StreamReader sr = new StreamReader(xmlPath))
				{
					XmlSerializer xmldes = new XmlSerializer(type);
					result = xmldes.Deserialize(sr);
				}
			}
			catch (Exception)
			{
				LogHelper.Log("Deserialize反序列化失败");
			}
			return result;
		}
	}
}
