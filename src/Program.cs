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
                
                //generate data
                Sim sim;
                using (ConsoleEx.WriteExecutionTime($"Generate Simulation Data (Scope: {settings.Scope})"))
                {
                    sim = SimEngine.CreateNewSim(settings);
                }
                
                //run iterations
                for (var i = 0; i < 100; i++)
                {
                    using (ConsoleEx.WriteExecutionTime($"Iteration [{sim.SimStates.Count}]"))
                    {
                        SimEngine.IterateSimulation(sim);
                        Console.WriteLine("Healthy: " + sim.SimStates[^1].CntHealthy);
                        Console.WriteLine("Infected: " + sim.SimStates[^1].UnknownTotalInfected);
                        Console.WriteLine("Incidence: " + sim.SimStates[^1].UnknownIncidence);
                        Console.WriteLine("Dead: " + sim.SimStates[^1].CntDead);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}