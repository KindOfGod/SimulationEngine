namespace SimulationEngine.PandemicEngine
{
    /// <summary>
    /// SimHelper contains Methods to assist the SimEngine.
    /// </summary>
    public static class SimHelper
    {
        //todo: use random seed! static seed only for debug use
        private const int Seed = 1;
        private static readonly Random Rnd = new(Seed);
        
        /// <summary>
        /// Merges two dictionaries with no duplicate key values.
        /// </summary>
        public static void MergeDictionariesNoDuplicates(Dictionary<uint, uint> destination,
            Dictionary<uint, uint> source)
        {
            if (source.Count == 0)
                return;

            source.ToList().ForEach(x => destination.Add(x.Key, x.Value));
        }
        
        /// <summary>
        /// Decide a true/false decision
        /// </summary>
        public static bool DecideWithProbability(double percentage)
        {
            var num = Rnd.Next(0, 100);
            return num <= percentage * 100;
        }
        
        /// <summary>
        /// Decide a percentage with a random deviation
        /// </summary>
        public static double DecidePercentageWithDeviation(uint count, double percentage, double deviation)
        {
            var dev = deviation * 100;
            var resDev = Rnd.Next(-(int)dev, (int)dev);

            return count * ((percentage * 100) + resDev);
        }
    }
}