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
        public static void MergeDictionariesNoDuplicates(Dictionary<uint, uint> destination, Dictionary<uint, uint> source)
        {
            if (source.Count == 0)
                return;
            
            source.ToList().ForEach(x => destination.Add(x.Key, x.Value));
        }
    }
}