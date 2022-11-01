using System;
using System.Diagnostics;
using SimulationEngine.src.Logging;

namespace SimulationEngine.src.Logging
{
    public class ExecutionTimeLogger : IDisposable
    {
        private readonly string _message;

        private Stopwatch _stopwatch;
        private readonly ILogger _logger;
        private readonly LogLevel _logLevel;

        public ExecutionTimeLogger(ILogger logger, LogLevel logLevel, string message)
        {
            _logger = logger;
            _logLevel = logLevel;
            _message = message;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Dispose()
        {
            
            _stopwatch.Stop();

            if(_logger.IsLoggingEnabled || _logLevel != LogLevel.Debug)
                _logger?.Log(_logLevel, $"{_message,-50} -->{_stopwatch.ElapsedMilliseconds,8}ms");

            _stopwatch = null;
        }
    }
}