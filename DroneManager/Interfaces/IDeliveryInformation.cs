using DroneManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Interfaces
{
    public interface IDeliveryInformation
    {


         CoordinateLetter2D[][] Positions { get; }
       
         DateTime ExecutionDate { get; }


    }
}
