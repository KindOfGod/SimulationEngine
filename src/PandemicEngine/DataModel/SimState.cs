namespace SimulationEngine.PandemicEngine.DataModel
{
    /// <summary>
    /// Contains information about a single iteration of a simulation.
    /// </summary>
    public class SimState
    {
        public Dictionary<uint, uint> PopIndex;

        public SimState(int scope)
        {
            PopIndex = new Dictionary<uint, uint>(scope / 100);
        }
    }
}