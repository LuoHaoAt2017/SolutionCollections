using log4net;

namespace DotNetADO
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
