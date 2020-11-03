using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Exceptions
{
    public class DuplicateDroneItem: Exception
    {
        public DuplicateDroneItem():base() { 
        
        }

        public DuplicateDroneItem(string message) : base(message)
        {

        }
        public DuplicateDroneItem(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
