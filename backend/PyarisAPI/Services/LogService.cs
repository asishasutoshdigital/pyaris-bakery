using log4net;

namespace PyarisAPI.Services
{
    public class LogService
    {
        private static readonly LogService _instance = new LogService();
        protected ILog ExceptionLogger;
        protected static ILog debugLogger;

        private LogService()
        {
            ExceptionLogger = LogManager.GetLogger("MonitoringLogger");
            debugLogger = LogManager.GetLogger("DebugLogger");
        }

        public static void Debug(string message)
        {
            debugLogger?.Debug(message);
        }

        public static void Debug(string message, Exception exception)
        {
            debugLogger?.Debug(message, exception);
        }

        public static void Error(string message)
        {
            _instance.ExceptionLogger?.Error(message);
        }

        public static void Error(string message, Exception exception)
        {
            _instance.ExceptionLogger?.Error(message, exception);
        }
    }
}
