using DroneManager.Model;
using FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuCorrientazoDomicilioBussiness.File.DataAccess
{
    public class DroneResultWritter : IFileWritter<DeliveryInformation>
    {
        public DeliveryInformation objecttoserialize { get; private set; }

        public bool isDone  { get; private set;}

        public DroneResultWritter(DeliveryInformation dev) {
            objecttoserialize = dev;
            isDone = false;
        }

        private int index = 0;


        private Vector2dInt last_delivery = Vector2dInt.Zero;
        public string nextNextLine()
        {
            string result = "";
            

            var delivery_position =   objecttoserialize.Positions[index].LastOrDefault();

          

               
            last_delivery = last_delivery+ delivery_position.Position;
               
              
           result = ( last_delivery).ToString()+ delivery_position.Angle;

        

            index++;
            isDone = index == objecttoserialize.Positions.Length;
            return result;
        }
    }
}
