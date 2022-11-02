using SimulationEngine.src.PandemicEngine.DataModel;

namespace SimulationEngine.src.PandemicEngine
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