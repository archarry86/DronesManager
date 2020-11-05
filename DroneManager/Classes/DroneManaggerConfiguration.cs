using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Classes
{
    /// <summary>
    /// This class encapsulates each configuration to manage the drones
    /// using a polimorfic reference we can have a dinamic implementation
    /// 
    /// A sub class may load a configuration from an local xml or plain file
    /// or  may load a configuration  from a web service 
    /// </summary>
    public abstract class DroneManaggerConfiguration
    {

        public abstract int GetMax_Items_Capacity();

        public abstract double GetMax_Weight_Drone_Capacity();

        /// <summary>
        /// Represents the half length of the maximun disntance 
        /// to travel
        /// </summary>
        /// <returns></returns>
        public abstract double GetMax_Cordinate_Value();
    }
}
