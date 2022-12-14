using System;
using System.Xml.Serialization;
using System.IO;

namespace Configuration.Common
{
	internal class Utilities
	{
		public static T GetConfigInfo<T>(string xmlPath) where T : class
		{
			T configInfo = default(T);
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
