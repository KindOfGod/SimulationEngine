using SimulationEngine.PandemicEngine.DataModel;

namespace SimulationEngine.PandemicEngine
{
    public static partial class SimEngine
    {
        
        public static Sim CreateNewSim(SimSettings settings)
        {
            return new Sim(settings, new List<SimState>{GenerateInitialSimState(settings)});
        }
        
        public static Sim LoadSim(SimSettings settings, List<SimState> simStates)
        {
            return new Sim(settings, simStates);
        }
    }
}