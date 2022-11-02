// ReSharper disable ShiftExpressionZeroLeftOperand
namespace SimulationEngine.PandemicEngine
{
    /// <summary>
    /// All states are saved in 32bit ulong.
    /// Least significant bit is first bit.
    /// </summary>
    
    // Bit 1-3 (3bit)
    public enum StateOfLife : uint
    {
        Healthy = 0,
        ImperceptiblyIll = 1,
        Ill = 2,
        HeavilyIll = 3,
        Dead = 4
    }

    // Bit 4-5 (2bit)
    public enum Age : uint
    {
        Child = 0 << 3,
        YoungAdult = 1 << 3,
        Adult = 2 << 3,
        Old = 3 << 3
    }
}