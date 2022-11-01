namespace SimulationEngine.src.Logging.Logger
{
    public class ConsoleLogger : ILogger
    {
        public bool IsLoggingEnabled {get; set;} = true;
        public void Log(LogLevel logLevel, string message)
        {
            Console.WriteLine(message);
        }
    }
}