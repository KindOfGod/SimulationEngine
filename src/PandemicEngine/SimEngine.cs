using SimulationEngine.PandemicEngine.DataModel;

namespace SimulationEngine.PandemicEngine
{
    public static class SimEngine
    {
        #region Pubic Methods

        public static Sim CreateSim(SimSettings settings)
        {
            return new Sim();
        }

        #endregion
    }
}