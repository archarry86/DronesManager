using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Classes
{
    /// <summary>
    /// A Hardcoded Implementation of DroneManaggerConfiguration using the restriccions of the excersice
    /// </summary>
    internal class DefaultDroneManaggerConfiguration : DroneManaggerConfiguration
    {
        public override int GetMax_Items_Capacity()
        {
            return 3;
        }

        public override double GetWeight_Items_Capacity()
        {
            return 0;
        }
    }
}
