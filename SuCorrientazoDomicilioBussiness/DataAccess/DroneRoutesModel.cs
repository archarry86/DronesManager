using DroneManager.Model.Drone;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuCorrientazoDomicilioBussiness.DataAccess
{
    /// <summary>
    /// An abstract class to obtain the routes of the drones
    /// I was going to  call this class DroneRoutesDao but I winded up calling it DroneRoutesModel
    /// 
    /// the subclases must fetch the DroneRoutes information from the file system
    /// ,or dataBase ,or weberservice, or ftp etc
    /// 
    /// 
    /// </summary>
    public abstract class DroneRoutesModel
    {

        public abstract IEnumerable<Drone> ReadDronesRouteInformation();

        public abstract void WriteDroneRouteReport(IEnumerable<Drone> drones);

    }
}
