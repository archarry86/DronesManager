using DroneManager.Classes;
using DroneManager.Model;
using DroneManager.Model.Drone;
using SuCorrientazoDomicilioBussiness.FileManager.Classes;
using SuCorrientazoDomicilioBussiness.FileManager.Implementations;
using SuCorrientazoDomicilioBussiness.FileManager.Interfaces;
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

            //All drones go out today at non
            //I suppose
            var date = DateTime.Today.AddHours(12);


            DirectoryInfo DirectoryInfo = new DirectoryInfo(FileManager.Classes.FileManager.MyDirectoryFiles);
            long indexcounter = 0;
            int counter = 0;
            foreach (var file in DirectoryInfo.GetFiles())
            {

                FileDroneLocationReader reader = new FileDroneLocationReader(FileManager.Classes.FileManager.MyDirectoryFiles);
                var drone_serial = reader.DroneSerial(file.Name);

                var cordinates = reader.ReadInformation(file.Name);


                var items = Enumerable.Range(0, cordinates.Length)
                        .Select((item, index) => Item.CreateItem(indexcounter+index + 1, DroneManager.Enums.ITemType.Lunch))
                        .ToList();

       


                DroneBuilder builder = new DroneBuilder()
                {
                    droneid = drone_serial,
                    max_items_capacity = droneManager.Configuration.GetMax_Items_Capacity(),
                    max_weight_capacity = droneManager.Configuration.GetMax_Weight_Drone_Capacity(),

                    _drone_items = items,
                 
                    deliveryInformation = new DeliveryInformation(cordinates, date)
                };


                droneManager.AddDrone(builder.Build());
                indexcounter += items.Count;
                counter++;
            }
        }


        public void WriteDispatchedDroneInformation()
        {

            foreach (var drone in droneManager.GetDrones()) {

                IFileWritter<DeliveryInformation> _writter = new DroneResultWritter(drone.deliveryInformation);
                var path = Path.Combine(FileManager.Classes.FileManager.MyDirectoryFilesResult, drone.deliveryInformation.ExecutionDate.ToString("yyyyMMdd"));
                directoryWritten = path;
                FileWritter<DeliveryInformation> writter = new FileWritter<DeliveryInformation>(path, _writter);

                writter.WriteInformation($"out{drone.DroneId}.txt");
            
            }
        }

        public String directoryWritten { get; private set; }

    }
}
