using log4net;

namespace PyarisAPI.Services
{
    public class LogService
    {
        private readonly ILog _exceptionLogger;
        private readonly ILog _debugLogger;

        public LogService()
        {
            _exceptionLogger = LogManager.GetLogger("MonitoringLogger");
            _debugLogger = LogManager.GetLogger("DebugLogger");
        }

        public void Debug(string message)
        {
            _debugLogger?.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _debugLogger?.Debug(message, exception);
        }

        public void Error(string message)
        {
            _exceptionLogger?.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _exceptionLogger?.Error(message, exception);
        }
    }
}
