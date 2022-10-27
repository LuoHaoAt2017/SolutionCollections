using log4net;

namespace Configuration.Common
{
	internal class LogHelper
	{
		private static readonly ILog logger = LogManager.GetLogger("FileLogger");

		public static void Log(string mesg)
		{
			logger.Info(mesg);
		}
	}
}
