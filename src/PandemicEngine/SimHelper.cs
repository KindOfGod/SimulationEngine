namespace SimulationEngine.PandemicEngine
{
    public static class SimHelper
    {
        public static uint AttributesToUnit()
        {
            uint store = 0;

            return store;
        }

        public static void MergeDictionariesNoDuplicates(Dictionary<uint, uint> destination, Dictionary<uint, uint> source)
        {
            if (source.Count == 0)
                return;
            
            source.ToList().ForEach(x => destination.Add(x.Key, x.Value));
        }
    }
}