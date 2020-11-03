using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Exceptions
{
    public class InvalidDroneCreationException: Exception
    {
        public InvalidDroneCreationException():base() { 
        
        }

        public InvalidDroneCreationException(string message) : base(message)
        {

        }
        public InvalidDroneCreationException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
