using System.Diagnostics;

namespace SimulationEngine
{
    public class ConsoleEx
    {
        public static IDisposable WriteExecutionTime(string message)
        {
            return new ExecutionTimeLogger(message);
        }
    }

    public class ExecutionTimeLogger : IDisposable
    {
        private readonly string _message;

        private Stopwatch _stopwatch;

        public ExecutionTimeLogger(string message)
        {
            _message = message;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            Console.WriteLine($"{_message,-50} -->{Math.Round(_stopwatch.Elapsed.TotalMilliseconds, 2).ToString("N2"),8}ms");
            _stopwatch = null;
        }
    }
}

