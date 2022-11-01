using log4net;

namespace ThreadDemo
{
	internal class LogHelper
	{
		private static readonly ILog infoLogger = LogManager.GetLogger("FileLogger");

		private static readonly ILog errLogger = LogManager.GetLogger("ErrorLogger");

		public static void Log(string mesg)
		{
			if (infoLogger.IsInfoEnabled)
			{
				infoLogger.Info(mesg);
			}
		}

		public static void Error(string mesg)
		{
			if (errLogger.IsErrorEnabled)
			{
				errLogger.Error(mesg);
			}
		}
	}
}
