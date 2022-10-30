using log4net;

namespace DotNetADO
{
	internal class LogHelper
	{
		private static readonly ILog infoLogger = LogManager.GetLogger("FileLogger");

		private static readonly ILog errLogger = LogManager.GetLogger("ErrorLogger");

		public static void Log(string mesg)
		{
			infoLogger.Info(mesg);
		}

		public static void Error(string mesg)
		{
			errLogger.Error(mesg);
		}
	}
}
