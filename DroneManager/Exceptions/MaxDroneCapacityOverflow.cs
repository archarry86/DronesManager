using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Exceptions
{
    public class MaxDroneCapacityOverflow: Exception
    {
        public MaxDroneCapacityOverflow():base() { 
        
        }

        public MaxDroneCapacityOverflow(string message) : base(message)
        {

        }
        public MaxDroneCapacityOverflow(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
