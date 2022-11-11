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
                    Scope = 100_000
                };

                //check validity
                
                var valid = settings.IsConfigurationValid();
                Console.ForegroundColor = valid == null ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"IsConfigValid: {valid == null}");
                
                if (valid != null)
                {
                    foreach (var str in valid)
                        Console.WriteLine(str);
                    
                    return;
                }
                Console.ForegroundColor = ConsoleColor.White;

                using (ConsoleEx.WriteExecutionTime($"Generate Simulation Data (Scope: {settings.Scope})"))
                {
                    var sim = SimEngine.CreateNewSim(settings);
                    
                    SimEngine.IterateSimulation(sim);
                }
            }
        }
    }
}