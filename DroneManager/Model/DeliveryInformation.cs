using DroneManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroneManager.Model
{
    public class DeliveryInformation : IDeliveryInformation
    {


        public CoordinateLetter2D[][] Positions { get; private set;}



        public DateTime ExecutionDate { get; private set; }

        public DeliveryInformation(CoordinateLetter2D[][] positions, DateTime executionDate) {


            Positions = positions;

            if (Positions.Any(p => p == null || p.Any(sp => sp == null) )) {

                throw new ArgumentNullException("All the positions should be diferent of null");
            }

           
            ExecutionDate = executionDate;

            //DateTime is an Struct in c# a Struct is not an object
            //so this information is not in the Heap
            //As a result I am validating that the execution is not equal to a defautl value
            if (ExecutionDate == DateTime.MinValue)
            {

                throw new ArgumentException("Invalid Date Parameter");
            }


        }


    }
}
