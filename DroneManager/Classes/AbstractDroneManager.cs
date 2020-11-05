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
    public abstract class AbstractDroneManager
    {
        /// <summary>
        /// Structure to manage Drones just by its id
        /// </summary>
        protected HashSet<Drone> drones = new HashSet<Drone>();

        protected DroneManaggerConfiguration _configuration = null;

        public DroneManaggerConfiguration Configuration {
            get {
                return _configuration;
            }
        }
        public AbstractDroneManager()
        {
            this._configuration = new DefaultDroneManaggerConfiguration();
        }

        public AbstractDroneManager(DroneManaggerConfiguration configuration)
        {

            this._configuration = configuration;
        }

        public abstract Drone CreateDrone(String Serial);



        public abstract void AddDrone(Drone drone);


        public abstract void AddItemToaDrone(Item item, string Serial);


        public abstract void AssingTodayDeliveryInformation( string Serial, CoordinateLetter2D[][] coordinates );

        public Drone FindDrone(string id_drone)
        {
            //atomic thread safe operation
            var drone = drones.FirstOrDefault(p => p.DroneId == id_drone);
            if (drone == null)
            {
                //this hosuld be a type of excpetion
                throw new ArgumentException($"The Drone {id_drone} has not been found");
            }


            return drone;
        }


        public int NumberOfDrones() {
            return drones.Count;
        }

        public IEnumerable<Drone> GetDrones() {
            return drones;
        }

        /// <summary>
        /// There is no implicit realtion between the item that the Dron Carry and the information to deliver
        /// But both are set in the same index of a two diferent collecitons as a result I create this method
        /// to get the item an the route to deliver it
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract  (CoordinateLetter2D[], Item) GetItemRoute(String serial, int index);


        /// <summary>
        /// The drone is not supposed to go far than a certain distance
        /// Validate that the abosolute value of cordinates to deliver  the item
        /// if the configuration was set
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public abstract bool ValidateDroneItemRoute(String serial ,int itemIndex);
    }


}
