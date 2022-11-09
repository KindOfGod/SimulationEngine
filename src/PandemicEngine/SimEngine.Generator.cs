using SimulationEngine.PandemicEngine.DataModel;

namespace SimulationEngine.PandemicEngine;

public static partial class SimEngine
{
    /// <summary>
    /// Generate a Initial Simulation State with valid SimSettings
    /// </summary>

    //
    // Generation works in a tree scheme. The bitfields of the PopIndex gets assembled by single attributes. 
    //
    private static SimState GenerateInitialSimState(SimSettings settings)
    {
        var popIndex = new Dictionary<uint, uint>();

        SimHelper.MergeDictionariesNoDuplicates(popIndex, GenerateAgeGroup(Age.Child,(uint)(settings.Scope * settings.AgeProportionOfChildren), settings));
        SimHelper.MergeDictionariesNoDuplicates(popIndex, GenerateAgeGroup(Age.YoungAdult,(uint)(settings.Scope * settings.AgeProportionOfYoungAdults), settings));
        SimHelper.MergeDictionariesNoDuplicates(popIndex, GenerateAgeGroup(Age.Adult,(uint)(settings.Scope * settings.AgeProportionOfAdults), settings));
        SimHelper.MergeDictionariesNoDuplicates(popIndex, GenerateAgeGroup(Age.Pensioner,(uint)(settings.Scope * settings.AgeProportionOfPensioner), settings));
        
        var state = new SimState(settings.Scope)
        {
            PopIndex = popIndex
        };
        
        return state;
    }

    private static Dictionary<uint, uint> GenerateAgeGroup(Age age, uint count, SimSettings settings)
    {
        var popIndex = new Dictionary<uint, uint>();
        var popState = (uint)( 0 | age);
        
        if (count == 0)
            return popIndex;
        
        //calculate illCount dependent of AgeGroup
        var illCount = (uint) 0;

        switch (age)
        {
            case Age.Child:
                illCount = (uint)(count * settings.InitialProportionOfInfectedChildren);
                break;
            case Age.YoungAdult:
                illCount = (uint)(count * settings.InitialProportionOfInfectedYoungAdults);
                break;
            case Age.Adult:
                illCount = (uint)(count * settings.InitialProportionOfInfectedAdults);
                break;
            case Age.Pensioner:
                illCount = (uint)(count * settings.InitialProportionOfInfectedPensioner);
                break;
        }

        SimHelper.MergeDictionariesNoDuplicates(popIndex, GenerateStateOfLife(StateOfLife.Healthy, popState, (count - illCount), settings));
        SimHelper.MergeDictionariesNoDuplicates(popIndex, GenerateStateOfLife(settings.HealthIllnessSeverity, popState, illCount, settings));

        return popIndex;
    }
    
    private static Dictionary<uint, uint> GenerateStateOfLife(StateOfLife sol, uint pop, uint count, SimSettings settings)
    {
        var popIndex = new Dictionary<uint, uint>();
        var popState =  pop | (uint)sol;
        
        if (count == 0)
            return popIndex;
        
        popIndex.Add(popState, count);

        return popIndex;
    }
}