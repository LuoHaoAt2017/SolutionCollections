using System;
using System.IO;
using System.Windows.Forms;
using Configuration.Models;
using Configuration.Common;

namespace Configuration
{
	public partial class Film : Form
	{
		private ConfigurationInfo configuration = null;

		public Film()
		{
			InitializeComponent();
			configuration = GetConfigurationInfo();
			LogHelper.Log($"designer: {configuration.machine.designer}");
			LogHelper.Log($"manufacturer: {configuration.machine.manufacturer}");
			LogHelper.Log($"location: {configuration.machine.location.province}-{configuration.machine.location.city}-{configuration.machine.location.region}");
		}

		public ConfigurationInfo GetConfigurationInfo()
		{
			ConfigurationInfo configurationInfo = null;
			string path = AppDomain.CurrentDomain.BaseDirectory + "configuration.xml";
			if (File.Exists(path))
			{
				configurationInfo = Utilities.GetConfigInfo<ConfigurationInfo>(path);
			}
			else
			{
				LogHelper.Log($"{path} 不存在");
			}
			return configurationInfo;
		}
	}
}
