using log4net;

namespace Multithreading
{
	internal class LogHelper
	{
		private static readonly ILog InfoLogger = LogManager.GetLogger("FileLogger");

		private static readonly ILog ErrLogger = LogManager.GetLogger("ErrorLogger");

		public static void Log(string mesg)
		{
			InfoLogger.Info(mesg);
		}

		public static void Error(string mesg)
		{
			ErrLogger.Error(mesg);
		}
	}
}
