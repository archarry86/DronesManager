using DroneManager.Interfaces;
using DroneManager.Model;
using DroneManager.Model.Drone;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace DroneManager.Classes
{
    public class DefaultDroneManager : AbstractDroneManager
    {

 

        public DefaultDroneManager() : base()
        {

        }

        public DefaultDroneManager(DroneManaggerConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Create Drone or returns its intance
        /// </summary>
        /// <returns></returns>
        public override Drone CreateDrone(String Serial)
        {
            //atomic thread safe operation

            //this local implementation will use the generated thread safe number  


            Drone drone = new Drone(Serial, this._configuration.GetMax_Items_Capacity(), this._configuration.GetWeight_Items_Capacity());
           
            var flag = drones.Add(drone);

            if (!flag)
            {
                //this may throw an exception due to modification by multi thread execution
                drone = FindDrone(Serial);
            }
            return drone;
        }


        public override void AddItemToaDrone(Item item, string serial)
        {

            var drone = FindDrone(serial);



            drone.AddItem(item);
        }

        public override void AssingTodayDeliveryInformation(string serial, CoordinateLetter2D[][] coordinates)
        {
            var drone = FindDrone(serial);



            drone.deliveryInformation = new DeliveryInformation(coordinates, DateTime.Now);


        }
    }
}
