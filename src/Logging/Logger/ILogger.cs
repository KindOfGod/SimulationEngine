namespace SimulationEngine.src.Logging
{
    public interface ILogger
    {
        bool IsLoggingEnabled {get; set;}
        void Log(LogLevel logLevel, string message);
    }
}