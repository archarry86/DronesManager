using DroneManager.Classes;
using DroneManager.Model;
using DroneManager.Model.Drone;
using SuCorrientazoDomicilioBussiness.DataAccess;
using SuCorrientazoDomicilioBussiness.File.DataAccess;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SuCorrientazoDomicilioBussiness
{
    public class SuCorrientazoDispatcher
    {

        private AbstractDroneManager droneManager;

        public AbstractDroneManager DroneManagerInstance { get { return droneManager;  } }

      
        public SuCorrientazoDispatcher()
        {
            droneManager = new DefaultDroneManager();


        }

        public void LoadDroneInformationToDispatch()
        {
            DroneRoutesModel model = new DataAccess.File.DroneRoutesModelFile(droneManager.Configuration);

           var drones  =  model.ReadDronesRouteInformation();
            foreach (var drone in drones)
                DroneManagerInstance.AddDrone(drone);

        }


        public void WriteDispatchedDroneInformation()
        {
            DroneRoutesModel model = new DataAccess.File.DroneRoutesModelFile(droneManager.Configuration);


            model.WriteDroneRouteReport(droneManager.GetDrones());

        }



    }
}
