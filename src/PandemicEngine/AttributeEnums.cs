namespace SimulationEngine.src.PandemicEngine
{
    /// <summary>
    /// People Attributes are represented in a 32bit field.
    /// </summary>

    // Bit 
    public enum StateOfLife
    {
        Healthy,
        ImperceptiblyIll,
        Ill,
        HeavilyIll,
        Dead
    }
}