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
                if(!AttributeHelper.CheckStateOfLive(pop.Key, StateOfLife.Dead))
                    SimHelper.MergeDictionaries(newPopIndex, IteratePip(pop.Key, pop.Value, sim));
                else
                    SimHelper.AddValueToDictionary(newPopIndex, pop.Key, pop.Value);
            }

            newState.PopIndex = newPopIndex;
            sim.SimStates.Add(newState);
            
            CalculateStateStats(sim);
        }

        /// <summary>
        /// Iterates a single Pop and returns Dictionary with resulting Pops
        /// </summary>
        private static Dictionary<uint, uint> IteratePip(uint pop, uint count, Sim sim)
        {
            var newPopIndex = new Dictionary<uint, uint>();
            var settings = sim.SimSettings;
            var prevState = sim.SimStates[^1];
            
            var isEndangeredAge = false;
            if(settings.EndangeredAgeGroup != null)
                isEndangeredAge = (pop & (uint)settings.EndangeredAgeGroup) > 0;
            
            //healthy
            if (AttributeHelper.CheckStateOfLive(pop, StateOfLife.Healthy))
            {
                var rateOfInfection = isEndangeredAge ? settings.EndangeredAgeInfectionRate : settings.BaseInfectionRate;
                
                //calculate modifier based on infection count
                rateOfInfection = rateOfInfection + settings.InfectionSpreadRate * rateOfInfection * ((double)prevState.UnknownTotalInfected / (settings.Scope - prevState.CntDead));
                
                var newInfected = SimHelper.DecideCountWithDeviation(count, rateOfInfection, settings.ProbabilityDeviation);
                
                SimHelper.AddValueToDictionary(newPopIndex, AttributeHelper.OverrideStateOfLive(pop, settings.InfectionSeverity), newInfected);
                SimHelper.AddValueToDictionary(newPopIndex, pop, count - newInfected);
            }
            //heavily infected
            else if (AttributeHelper.CheckStateOfLive(pop, StateOfLife.HeavilyInfected))
            {
                var rateOfDead = isEndangeredAge ? settings.EndangeredAgeDeathRate : settings.BaseDeathRate;
                
                var newDead = SimHelper.DecideCountWithDeviation(count, rateOfDead, settings.ProbabilityDeviation);
                
                SimHelper.AddValueToDictionary(newPopIndex, AttributeHelper.OverrideStateOfLive(pop, StateOfLife.Dead), newDead);
                SimHelper.AddValueToDictionary(newPopIndex, pop, count - newDead);
            }
            //infected
            else
            {
                var rateOfWorsening = isEndangeredAge ? settings.EndangeredAgeRateOfGettingWorse : settings.RateOfGettingWorse;
                
                var newWorse = SimHelper.DecideCountWithDeviation(count, rateOfWorsening, settings.ProbabilityDeviation);
                var severity = AttributeHelper.CheckStateOfLive(pop, StateOfLife.ImperceptiblyInfected) ?
                    StateOfLife.Infected : 
                    StateOfLife.HeavilyInfected;

                SimHelper.AddValueToDictionary(newPopIndex, AttributeHelper.OverrideStateOfLive(pop, severity), newWorse);
                SimHelper.AddValueToDictionary(newPopIndex, pop, count - newWorse);
            }

            return newPopIndex;
        }
        
        /// <summary>
        /// Calculates Stats for last SimState
        /// </summary>
        private static void CalculateStateStats(Sim sim)
        {
            var prevState = sim.SimStates[^2];
            var state = sim.SimStates[^1];
            foreach (var (key, count) in state.PopIndex)
            {
                if (AttributeHelper.CheckStateOfLive(key, StateOfLife.ImperceptiblyInfected))
                    state.CntImperceptibleInfected += Convert.ToInt64(count);
                else if (AttributeHelper.CheckStateOfLive(key, StateOfLife.Infected))
                    state.CntInfected += Convert.ToInt64(count);
                else if (AttributeHelper.CheckStateOfLive(key, StateOfLife.HeavilyInfected))
                    state.CntHeavilyInfected += Convert.ToInt64(count);
                else if (AttributeHelper.CheckStateOfLive(key, StateOfLife.Dead))
                    state.CntDead += Convert.ToInt64(count);
                else
                    state.CntHealthy += Convert.ToInt64(count);

                state.TotalInfected = state.CntInfected + state.CntHeavilyInfected;
                state.UnknownTotalInfected = state.CntImperceptibleInfected + state.CntInfected + state.CntHeavilyInfected;

                var rec = state.CntHealthy - prevState.CntHealthy;
                state.Recovered = rec >= 0 ? rec : 0;

                var unknownInc = prevState.CntHealthy - state.CntHealthy;
                state.UnknownIncidence = unknownInc >= 0 ? unknownInc : 0;

                var inc = prevState.CntHealthy + prevState.CntImperceptibleInfected - state.CntHealthy - prevState.CntImperceptibleInfected;
                state.Incidence = inc >= 0 ? inc : 0;

                state.DeathRate = state.CntDead - prevState.CntDead;
            }
        }
    }
}