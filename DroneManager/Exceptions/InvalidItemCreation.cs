using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Exceptions
{
    public class InvalidItemCreation: Exception
    {
        public InvalidItemCreation():base() { 
        
        }

        public InvalidItemCreation(string message) : base(message)
        {

        }
        public InvalidItemCreation(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
