using DroneManager.Model;
using SuCorrientazoDomicilioBussiness.FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuCorrientazoDomicilioBussiness.FileManager.Implementations
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

        public string nextNextLine()
        {
            string result = "";

            var last =   objecttoserialize.Positions[index].LastOrDefault();

            if(last != null)
            result = last.Position_String();

            index++;
            isDone = index == objecttoserialize.Positions.Length;
            return result;
        }
    }
}
