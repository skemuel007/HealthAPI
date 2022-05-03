﻿namespace HealthAPI.Repositories.Interfaces
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogTrace(string message);
        void LogFatal(string message);
    }
}
