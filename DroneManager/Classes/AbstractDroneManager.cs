﻿using DroneManager.Interfaces;
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
        public AbstractDroneManager()
        {
            this._configuration = new DefaultDroneManaggerConfiguration();
        }

        public AbstractDroneManager(DroneManaggerConfiguration configuration)
        {

            this._configuration = configuration;
        }

        public abstract Drone CreateDrone(String Serial);


        public abstract void AddItemToaDrone(Item item, string Serial);


        public abstract void AssingTodayDeliveryInformation( string Serial, CoordinateLetter2D[][] coordinates );

        public Drone FindDrone(string id_drone)
        {
            //atomic thread safe operation
            var drone = drones.FirstOrDefault(p => p.DroneId == id_drone);
            if (drone == null)
            {
                //this hosuld be a type of excpetion
                throw new Exception($"The Drone {id_drone} has not been found");
            }


            return drone;
        }


        public int NumberOfDrones() {
            return drones.Count;
        }

    }

    
}
