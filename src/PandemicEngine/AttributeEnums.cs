// ReSharper disable ShiftExpressionZeroLeftOperand
namespace SimulationEngine.PandemicEngine
{
    /// <summary>
    /// All states are saved in 32bit ulong.
    /// Least significant bit is first bit.
    /// </summary>
    ///
    /// To override a enum in uint: 
    /// (store & ~Compare) + newValue;
    ///
    /// To check for enum value
    /// store & Compare == Value
    
    
    // Bit 1-3 (3bit)
    public enum StateOfLife : uint
    {
        Healthy = 1,
        ImperceptiblyInfected = 2,
        Infected = 3,
        HeavilyInfected = 4,
        Dead = 5,
        
        Compare = 7
    }

    // Bit 4-6 (3bit)
    public enum Age : uint
    {
        Child = 1 << 3,
        YoungAdult = 2 << 3,
        Adult = 3 << 3,
        Pensioner = 4 << 3,
        
        Compare = 7 << 3
    }
}