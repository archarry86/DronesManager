using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Exceptions
{
    public class MaxWeightCapacityDroneException : Exception
    {

        /// <summary>
        /// Represents the amount of weight that the drone capacity has been exceded
        /// </summary>
        public double Excess { get; }

       
        public MaxWeightCapacityDroneException(string message, double excess) : base(message)
        {
            if(excess > 0 )
             this.Excess = excess;
        }
        public MaxWeightCapacityDroneException(string message, Exception innerException, double excess) : base(message, innerException)
        {
            if (excess > 0)
                this.Excess = excess;
        }

    }
}
