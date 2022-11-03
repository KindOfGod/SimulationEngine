namespace SimulationEngine.PandemicEngine.DataModel
{
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