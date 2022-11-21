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
                for (var i = 0; i < 1; i++)
                {
                    using (ConsoleEx.WriteExecutionTime($"Iteration [{sim.SimStates.Count}]"))
                    {
                        SimEngine.IterateSimulation(sim);
                    }
                }
                
            }
        }
    }
}