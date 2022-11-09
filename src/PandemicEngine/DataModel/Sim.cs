namespace SimulationEngine.PandemicEngine.DataModel
{
    /// <summary>
    /// Contains all information and settings from a single Simulation
    /// </summary>
    public class Sim
    {
        public SimSettings SimSettings { get; }
        public List<SimState> SimStates { get; }
        
        public Sim(SimSettings simSettings, List<SimState> simStates)
        {
            SimSettings = simSettings;
            SimStates = simStates;
        }
    }
}