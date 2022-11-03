using SimulationEngine.PandemicEngine;
using SimulationEngine.PandemicEngine.DataModel;

namespace SimulationEngine
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // TEST 
            using (ConsoleEx.WriteExecutionTime("Elapsed Simulation Time"))
            {
                var settings = new SimSettings()
                {
                    Scope = 10000,
                    InitialProportionOfInfectedChildren = 0
                };
                
                using (ConsoleEx.WriteExecutionTime($"Generate Simulation Data (Scope: {settings.Scope})"))
                {
                    var sim = SimEngine.CreateNewSim(settings);
                }
            }
            
            Console.WriteLine("END");
        }
    }
}