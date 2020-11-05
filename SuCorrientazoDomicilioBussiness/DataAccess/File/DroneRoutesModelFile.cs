using DroneManager.Model.Drone;
using System.Collections.Generic;


using DroneManager.Model;
using DroneManager.Model.Drone;
using SuCorrientazoDomicilioBussiness.File.DataAccess;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DroneManager.Classes;
using FileManager.Interfaces;
using FileManager.Classes;

namespace SuCorrientazoDomicilioBussiness.DataAccess.File
{
    public class DroneRoutesModelFile : DroneRoutesModel
    {
        private DroneManaggerConfiguration Configuration;

        public String directoryWritten { get; private set; }
        public DroneRoutesModelFile(DroneManaggerConfiguration configuration)
        {

            this.Configuration = configuration;
        }

        public override IEnumerable<Drone> ReadDronesRouteInformation()
        {
            try
            {


                DirectoryInfo DirectoryInfo = new DirectoryInfo(FileManager.Classes.FileManager.MyDirectoryFiles);
                long indexcounter = 0;
                int counter = 0;

                List<Drone> _list = new List<Drone>();

                var date = DateTime.Today.AddHours(12);


                foreach (var file in DirectoryInfo.GetFiles())
                {

                    FileDroneLocationReader reader = new FileDroneLocationReader(FileManager.Classes.FileManager.MyDirectoryFiles);


                    var drone_serial = reader.DroneSerial(file.Name);

                    var cordinates = reader.ReadInformation(file.Name);


                    var items = Enumerable.Range(0, cordinates.Length)
                            .Select((item, index) => Item.CreateItem(indexcounter + index + 1, DroneManager.Enums.ITemType.Lunch))
                            .ToList();


                    DroneBuilder builder = new DroneBuilder()
                    {
                        droneid = drone_serial,
                        max_items_capacity = Configuration.GetMax_Items_Capacity(),
                        max_weight_capacity = Configuration.GetMax_Weight_Drone_Capacity(),

                        _drone_items = items,

                        deliveryInformation = new DeliveryInformation(cordinates, date)
                    };

                    _list.Add(builder.Build());
                    //droneManager.AddDrone(builder.Build());
                    indexcounter += items.Count;
                    counter++;
                }

                return _list;
            }
            catch (IOException ex)
            {
                //Incase there may something specfic to do in this error
                //sofar this kind of exception will be rethrown
                throw;
            }
            catch (Exception ex)
            {
                //Incase there may something specfic to do in this error
                //sofar this kind of exception will be rethrown
                throw;
            }



        }

        public override void WriteDroneRouteReport(IEnumerable<Drone> drones)
        {
            try
            {
                foreach (var drone in drones)
                {

                    IFileWritter<DeliveryInformation> _writter = new DroneResultWritter(drone.DeliveryInformation);
                    var path = Path.Combine(FileManager.Classes.FileManager.MyDirectoryFilesResult, drone.DeliveryInformation.ExecutionDate.ToString("yyyyMMdd"));

                    FileWritter<DeliveryInformation> writter = new FileWritter<DeliveryInformation>(path, _writter);

                    writter.WriteInformation($"out{drone.DroneId}.txt");

                }


            }
            catch (IOException ex)
            {
                //Incase there may something specfic to do in this error
                //sofar this kind of exception will be rethrown
                throw;
            }
            catch (Exception ex)
            {
                //Incase there may something specfic to do in this error
                //sofar this kind of exception will be rethrown
                throw;
            }
        }
    }
}
