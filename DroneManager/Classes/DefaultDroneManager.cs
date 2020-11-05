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


            Drone drone = new Drone(Serial, this._configuration.GetMax_Items_Capacity(), this._configuration.GetMax_Weight_Drone_Capacity());
           
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
            //it would be nice if I validate that any Item  is not been sent in two drones 

            var drone = FindDrone(serial);



            drone.AddItem(item);
        }

        public override void AssingTodayDeliveryInformation(string serial, CoordinateLetter2D[][] coordinates)
        {
            var drone = FindDrone(serial);



            drone.DeliveryInformation = new DeliveryInformation(coordinates, DateTime.Now);


        }

        public override void AddDrone(Drone drone)
        {

            //it would be nice if I validate that any Item  is not been sent in two drones 

            var added = drones.Add(drone);

           
            if (!added){
                throw new ArgumentException($"The drone was already added serial {drone.DroneId}");
            }

        


        }

 
        public override (CoordinateLetter2D[] , Item ) GetItemRoute(String serial, int index)
        {

            Drone drone = FindDrone(serial);

            CoordinateLetter2D[] route = drone.DeliveryInformation.Positions[index];
            var item =  drone.GetItem(index);

            return (route, item);
        }

        public override bool ValidateDroneItemRoute(String serial, int itemIndex)
        {

            if (this.Configuration.GetMax_Cordinate_Value() > 0) {
                Drone drone = FindDrone(serial);
                var lastitem = drone.DeliveryInformation.Positions[itemIndex].Last();

                //validation within an square
                return
                    Math.Abs( lastitem.Position.X) <= this.Configuration.GetMax_Cordinate_Value() 
                    &&
                      Math.Abs(lastitem.Position.Y) <= this.Configuration.GetMax_Cordinate_Value()
                    ;

            }
            else {
                return true;
            }
            
        }
    }
}
