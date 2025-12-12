using log4net;

/// <summary>
/// Starting with used to log Exception in application.
/// </summary>
public class Log
{
    private static readonly Log _instance = new Log();
    protected ILog ExceptionLogger;
    protected static ILog debugLogger;
    private Log()
    {
        ExceptionLogger = LogManager.GetLogger("MonitoringLogger");
        debugLogger = LogManager.GetLogger("DebugLogger");
    }

    public static void Debug(string message)
    {
        debugLogger.Debug(message);
    }
    public static void Debug(string message, System.Exception exception)
    {
        debugLogger.Debug(message, exception);
    }
    public static void Error(string message)
    {
        _instance.ExceptionLogger.Error(message);
    }


    public static void Error(string message, System.Exception exception)
    {
        _instance.ExceptionLogger.Error(message, exception);
    }
}