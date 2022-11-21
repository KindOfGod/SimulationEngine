using SimulationEngine.PandemicEngine.DataModel;

namespace SimulationEngine.PandemicEngine
{
    /// <summary>
    /// The SimEngine contains all Methods to generate and run a simulation. The SimEngine does not save a single simulation.
    /// All methods can be used for multiple simulation at once.
    /// </summary>
    public static partial class SimEngine
    {
        /// <summary>
        /// Create and generate new simulation with SimSettings.
        /// </summary>
        public static Sim CreateNewSim(SimSettings settings)
        {
            return new Sim(settings, new List<SimState>{GenerateInitialSimState(settings)});
        }
        
        /// <summary>
        /// Load Simulation from SimSettings and SimStates.
        /// </summary>
        public static Sim LoadSim(SimSettings settings, List<SimState> simStates)
        {
            return new Sim(settings, simStates);
        }
        
        /// <summary>
        /// Iterate Simulation to next state
        /// </summary>
        public static void IterateSimulation(Sim sim)
        {
            var newState = new SimState(sim.SimSettings.Scope);
            var newPopIndex = new Dictionary<uint, uint>();

            foreach (var pop in sim.SimStates[^1].PopIndex)
            {
                //only simulate living people
                if((pop.Key & (uint) StateOfLife.Dead) == 0)
                    SimHelper.MergeDictionariesNoDuplicates(newPopIndex, IteratePip(pop.Key, pop.Value, sim.SimSettings));
                else
                    newPopIndex.Add(pop.Key, pop.Value);
            }
            sim?.SimStates.Add(newState);
        }

        /// <summary>
        /// Iterates a single Pop and returns Dictionary with resulting Pops
        /// </summary>
        private static Dictionary<uint, uint> IteratePip(uint pop, uint count, SimSettings settings)
        {
            var newPopIndex = new Dictionary<uint, uint>();
            
            var isEndangeredAge = false;
            if(settings.EndangeredAgeGroup != null)
                isEndangeredAge = (pop & (uint)settings.EndangeredAgeGroup) > 0;
            
            //healthy
            if ((pop & (uint) StateOfLife.Healthy) > 0)
            {
                var rateOfWorsening = isEndangeredAge ? settings.EndangeredAgeRateOfGettingWorse : settings.RateOfGettingWorse;
            }
            //infected
            else
            {
                var rateOfDead = isEndangeredAge ? settings.EndangeredAgeDeathRate : settings.BaseDeathRate;
            }

            return newPopIndex;
        }
    }
}