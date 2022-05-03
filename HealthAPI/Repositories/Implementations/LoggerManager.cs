using HealthAPI.Repositories.Interfaces;
using NLog;

namespace HealthAPI.Repositories.Implementations
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager() { }

        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message) => logger.Error(message);

        public void LogInfo(string message) => logger.Info(message);

        public void LogWarning(string message) => logger.Warn(message);
        public void LogTrace(string message) => logger.Trace(message);
        public void LogFatal(string message) => logger.Fatal(message);
    }
}
