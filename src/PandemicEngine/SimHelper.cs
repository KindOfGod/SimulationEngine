namespace SimulationEngine.PandemicEngine
{
    /// <summary>
    /// SimHelper contains Methods to assist the SimEngine.
    /// </summary>
    public static class SimHelper
    {
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
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var num = rnd.Next(0, 100);

            return num <= percentage * 100;
        }
        
        /// <summary>
        /// Decide a percentage with a random deviation
        /// </summary>
        public static double DecidePercentageWithDeviation(uint count, double percentage, double deviation)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            
            var dev = deviation * 100;
            var resDev = rnd.Next(-(int)dev, (int)dev);

            return count * ((percentage * 100) + resDev);
        }
    }
}