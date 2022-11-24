using log4net;

namespace Multithreading
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			FileInfo file = new FileInfo("log4net.config");
			log4net.Config.XmlConfigurator.Configure(file);
			ApplicationConfiguration.Initialize();
			Application.Run(new FrmMain());
		}
	}
}