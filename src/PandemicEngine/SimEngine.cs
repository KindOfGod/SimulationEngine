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
                //respect possible duplicates in dictionary
                if((pop.Key & (uint)StateOfLife.Compare) != (uint)StateOfLife.Dead)
                    SimHelper.MergeDictionaries(newPopIndex, IteratePip(pop.Key, pop.Value, sim.SimSettings));
                else
                    SimHelper.AddValueToDictionary(newPopIndex, pop.Key, pop.Value);
            }

            newState.PopIndex = newPopIndex;
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
            if ((pop & (uint)StateOfLife.Compare) == (uint)StateOfLife.Healthy)
            {
                var rateOfInfection = isEndangeredAge ? settings.EndangeredAgeInfectionRate : settings.BaseInfectionRate;
                
                var newInfected = SimHelper.DecideCountWithDeviation(count, rateOfInfection, settings.ProbabilityDeviation);
                
                SimHelper.AddValueToDictionary(newPopIndex, (pop & ~(uint)StateOfLife.Compare) + (uint)settings.InfectionSeverity, newInfected);
                SimHelper.AddValueToDictionary(newPopIndex, pop, count - newInfected);
            }
            //heavily infected
            else if ((pop & (uint)StateOfLife.Compare) == (uint)StateOfLife.HeavilyInfected)
            {
                var rateOfDead = isEndangeredAge ? settings.EndangeredAgeDeathRate : settings.BaseDeathRate;
                
                var newDead = SimHelper.DecideCountWithDeviation(count, rateOfDead, settings.ProbabilityDeviation);
                
                SimHelper.AddValueToDictionary(newPopIndex, (pop & ~(uint)StateOfLife.Compare) + (uint)StateOfLife.Dead, newDead);
                SimHelper.AddValueToDictionary(newPopIndex, pop, count - newDead);
            }
            //infected
            else
            {
                var rateOfWorsening = isEndangeredAge ? settings.EndangeredAgeRateOfGettingWorse : settings.RateOfGettingWorse;
                
                var newWorse = SimHelper.DecideCountWithDeviation(count, rateOfWorsening, settings.ProbabilityDeviation);
                var severity = (pop & (uint)StateOfLife.Compare) == (uint)StateOfLife.ImperceptiblyInfected ?
                    StateOfLife.Infected : 
                    StateOfLife.HeavilyInfected;

                SimHelper.AddValueToDictionary(newPopIndex, (pop & ~(uint)StateOfLife.Compare) + (uint)severity, newWorse);
                SimHelper.AddValueToDictionary(newPopIndex, pop, count - newWorse);
            }

            return newPopIndex;
        }
    }
}