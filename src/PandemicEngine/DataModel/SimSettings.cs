namespace SimulationEngine.PandemicEngine.DataModel
{
    /// <summary>
    /// Contains all dynamic Properties of a Simulation. Used to initialize a Sim.
    /// All Properties can be changed by the User and they have to contain a default value.
    /// </summary>
    public class SimSettings
    {
        #region People Configuration
        
        // General
        public int Scope { get; set; } = 100_000;
        
        // Health Distribution
        public double InitialProportionOfInfectedChildren { get; set; } = 0; // has to be >= 0 and <= 1
        public double InitialProportionOfInfectedYoungAdults { get; set; } = 0; // has to be >= 0 and <= 1
        public double InitialProportionOfInfectedAdults { get; set; } = 0; // has to be >= 0 and <= 1
        public double InitialProportionOfInfectedPensioner { get; set; } = 0; // has to be >= 0 and <= 1
        public StateOfLife HealthIllnessSeverity { get; set; } = StateOfLife.Ill; // can't be Healthy or Dead
        
        // Age Distribution --> has to be 1 in total
        public double AgeProportionOfChildren { get; set; } = 0.15; 
        public double AgeProportionOfYoungAdults { get; set; } = 0.1;
        public double AgeProportionOfAdults { get; set; } = 0.45;
        public double AgeProportionOfPensioner { get; set; } = 0.3;

        #endregion

        #region Virus Configuration

        

        #endregion

        #region Methods

        /// <summary>
        /// Returns List of errors as string or null if configuration is valid.
        /// </summary>
        public List<string>? IsConfigurationValid()
        {
            var errors = new List<string>();
            
            //Health
            
            if (InitialProportionOfInfectedChildren is not (>= 0 and <= 1))
                errors.Add("InitialProportionOfInfectedChildren is not (>= 0 or <= 1)");
            if (InitialProportionOfInfectedYoungAdults is not (>= 0 and <= 1))
                errors.Add("InitialProportionOfInfectedYoungAdults is not (>= 0 or <= 1)");
            if (InitialProportionOfInfectedAdults is not (>= 0 and <= 1))
                errors.Add("InitialProportionOfInfectedAdults is not (>= 0 or <= 1)");
            if (InitialProportionOfInfectedPensioner is not (>= 0 and <= 1))
                errors.Add("InitialProportionOfInfectedPensioner is not (>= 0 or <= 1)");
            
            if(HealthIllnessSeverity is StateOfLife.Dead or StateOfLife.Healthy)
                errors.Add("HealthIllnessSeverity can't be Dead or Healthy");
            
            //Age
            
            if(Math.Abs(AgeProportionOfChildren + AgeProportionOfYoungAdults + AgeProportionOfAdults +
                   AgeProportionOfPensioner - 1) > 0.01)
                errors.Add("AgeProportions sum is not 1");

            return errors.Count > 0 ? errors : null;
        }

        #endregion
    }
}