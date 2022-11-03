namespace SimulationEngine.PandemicEngine.DataModel
{
    public class SimState
    {
        public Dictionary<uint, uint> PopIndex;

        public SimState(int scope)
        {
            PopIndex = new Dictionary<uint, uint>(scope / 100);
        }
    }
}