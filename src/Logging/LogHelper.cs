using System;
using SimulationEngine.src.Logging;
using SimulationEngine.src.Logging.Logger;

namespace SimulationEngine.src.Logging
{
    public static class LogHelper
    {
        private static ILogger _defaultLogger;

        static LogHelper()
        {
            _defaultLogger = new ConsoleLogger();
        }

        public static IDisposable LogExecutionTime(LogLevel logLevel, string message, ILogger logger = null)
        {
            logger ??= _defaultLogger;

            return new ExecutionTimeLogger(logger, logLevel, message);
        }

        public static void OverrideDefaultLogger(ILogger logger)
        {
            _defaultLogger = logger;
        }
    }
}