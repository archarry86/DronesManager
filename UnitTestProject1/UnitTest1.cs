using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using DroneManager.Classes;
using DroneManager.Enums;
using DroneManager.Exceptions;
using DroneManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuCorrientazoDomicilioBussiness.FileManager.Classes;
using SuCorrientazoDomicilioBussiness.FileManager.Implementations;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        private Vector2dInt firstDelivery = new Vector2dInt(-2, 4);
        private Vector2dInt secondDelivery = new Vector2dInt(-1, 3);
        private Vector2dInt thirdDelivery = new Vector2dInt(0, 0);

        private CoordinateLetter2D[][] coordinates;

        private AbstractDroneManager abstractDroneManager;

        private List<Guid> dronesGui = new List<Guid>();

        [TestMethod]
        public void TestVectorRotations()
        {
            Vector2dInt vector = Vector2dInt.Right;

            Vector2dInt rotatiom = vector.Rotate(90);

            Assert.AreEqual(rotatiom, Vector2dInt.up);

            rotatiom = rotatiom.Rotate(90);

            Assert.AreEqual(rotatiom, Vector2dInt.left);

            rotatiom = rotatiom.Rotate(90);

            Assert.AreEqual(rotatiom, Vector2dInt.down);

            rotatiom = rotatiom.Rotate(90);

            Assert.AreEqual(rotatiom, Vector2dInt.Right);

        }





        [TestMethod]
        public void ValidateVectorFromAngle()
        {

            var result = Vector2dInt.VectorFromAngle(0);

            Assert.AreEqual(result, Vector2dInt.Right);
            result = Vector2dInt.VectorFromAngle(90);
            Assert.AreEqual(result, Vector2dInt.up);
            result = Vector2dInt.VectorFromAngle(180);
            Assert.AreEqual(result, Vector2dInt.left);
            result = Vector2dInt.VectorFromAngle(270);
            Assert.AreEqual(result, Vector2dInt.down);
        }

        [TestMethod]
        public void TestNormalCalculation()
        {
            Vector2dInt vector = Vector2dInt.Right;

            Vector2dInt normal = vector.CalculateNormal();

            Assert.AreEqual(normal, Vector2dInt.up);

            normal = normal.CalculateNormal();

            Assert.AreEqual(normal, Vector2dInt.left);

            normal = normal.CalculateNormal();

            Assert.AreEqual(normal, Vector2dInt.down);

            normal = normal.CalculateNormal();

            Assert.AreEqual(normal, Vector2dInt.Right);

        }


        [TestMethod]
        public void ValidateRouteCalulation()
        {
            this.coordinates = new CoordinateLetter2D[3][];
            //  AAAAIAA
          
            LeterCoordinates[] coordinates = new LeterCoordinates[]
            {
                LeterCoordinates.A,
                LeterCoordinates.A,
                LeterCoordinates.A,
                LeterCoordinates.A,
                LeterCoordinates.I,
                LeterCoordinates.A,
                LeterCoordinates.A

            };


            
            CoordinateLetter2D[] list = CreateList(coordinates);
            CoordinateLetter2D lastposition = list.Last();
            Assert.AreEqual(lastposition.Position, firstDelivery);
         
            int index = 0;
            this.coordinates[index] = list.ToArray();

            //DDDAIAD

            coordinates = new LeterCoordinates[]
            {
                LeterCoordinates.D,
                LeterCoordinates.D,
                LeterCoordinates.D,
                LeterCoordinates.A,
                LeterCoordinates.I,
                LeterCoordinates.A,
                LeterCoordinates.D

            };

          
            list = CreateList(coordinates, lastposition);
            lastposition = list.Last();
            Assert.AreEqual(lastposition.Position, secondDelivery);

            index++;
            this.coordinates[index] = list.ToArray();



        
            //AAIADAD
            coordinates = new LeterCoordinates[]
           {
                LeterCoordinates.A,
                LeterCoordinates.A,
                LeterCoordinates.I,
                LeterCoordinates.A,
                LeterCoordinates.D,
                LeterCoordinates.A,
                LeterCoordinates.D

           };

            list = CreateList(coordinates, lastposition);
            lastposition = list.Last();
          
            Assert.AreEqual(lastposition.Position,thirdDelivery);

            index++;
            this.coordinates[index] = list.ToArray();

            Assert.AreEqual(this.coordinates.Any(p=> p == null || p.Any(sp=> sp ==  null)), false);


        }

        private CoordinateLetter2D[] CreateList(LeterCoordinates[] coordinates, CoordinateLetter2D lastcoordinate = null)
        {
            CoordinateLetter2D[] list = new CoordinateLetter2D[coordinates.Length];

            int i = 0;
            foreach (var letter in coordinates)
            {

                var newCoorinate = new CoordinateLetter2D(letter, lastcoordinate);
                list[i]=newCoorinate;
                i++;
            //parameter reasingnation but I am no generating any crazy side effect
                lastcoordinate = newCoorinate;



            }

            return list;
        }


        [TestMethod]
        public void CreateDefaultManager()
        {
            if (abstractDroneManager == null) { 
                abstractDroneManager = new DefaultDroneManager();


                Assert.AreNotEqual(abstractDroneManager.NumberOfDrones(), null);

            }
        }



        [TestMethod]
        public void CreateDrones()
        {
            CreateDefaultManager();



            int number_of_drones = 10;

            for (int index = 1; index <= number_of_drones; index++) {
                abstractDroneManager.CreateDrone(index.ToString());
            }

            Assert.AreEqual(abstractDroneManager.NumberOfDrones(), number_of_drones);


            int number_of_drones_by_guid = 10;

            for (int index = 1; index <= number_of_drones; index++)
            {
                Guid id = new Guid();
                abstractDroneManager.CreateDrone(id.ToString());
                dronesGui.Add(id);
            }

            Assert.AreEqual(abstractDroneManager.NumberOfDrones(), number_of_drones+ number_of_drones_by_guid);



        }


        [TestMethod]
        public void AddItemstoADroneSerialOne()
        {

            if (abstractDroneManager == null || abstractDroneManager.NumberOfDrones() == 0)
                CreateDrones();

            long index = 0;
            index++;
            Item it = Item.CreateItem(index, ITemType.Lunch, new Random().Next(1,5));

            String serialone = "1";

            abstractDroneManager.AddItemToaDrone(it, serialone);

            //validate that i can not add a duplicate ittem to a drone
            Assert.ThrowsException<DuplicateDroneItem>(()=>abstractDroneManager.AddItemToaDrone(it, serialone));

            index++;
            it = Item.CreateItem(index, ITemType.Lunch, new Random().Next(1, 5));

           

            abstractDroneManager.AddItemToaDrone(it, serialone);

            index++;
            it = Item.CreateItem(index, ITemType.Lunch, new Random().Next(1, 5));



            abstractDroneManager.AddItemToaDrone(it, serialone);

            index++;
            it = Item.CreateItem(index, ITemType.Lunch, new Random().Next(1, 5));


            //validate that i can not add more items than the initial capacty
            Assert.ThrowsException<MaxDroneCapacityOverflow>(() => abstractDroneManager.AddItemToaDrone(it, serialone));


        }


        [TestMethod]
        public void ValidateParseLine()
        {
            if (this.coordinates == null )
                this.ValidateRouteCalulation();

            CoordinateLetter2DReader reader = new CoordinateLetter2DReader();
            var line = "AAAAIAA";

            var result = reader.read(line, 0);

            Assert.AreEqual(result.Last().Position, new Vector2dInt(-2, 4));
           

        }

        [TestMethod]
        public void ValidateFileWritting()
        {
            if (this.coordinates == null)
                this.ValidateRouteCalulation();


            DeliveryInformation deliveryInformation = new DeliveryInformation(this.coordinates, DateTime.Now);
            DroneResultWritter writter = new DroneResultWritter(deliveryInformation);

            StringBuilder builder = new StringBuilder();
            while (!writter.isDone)
            {
                builder.AppendLine(writter.nextNextLine());


            }

            String result = String.Join(Environment.NewLine,
            firstDelivery.ToString(),
             secondDelivery.ToString(),
             thirdDelivery.ToString() ) + Environment.NewLine;

            Assert.AreEqual(result ,builder.ToString());


        }


        [TestMethod]
        public void ValidateDispatcher()
        {
            if (this.coordinates == null)
                this.ValidateRouteCalulation();


            SuCorrientazoDomicilioBussiness.SuCorrientazoDispatcher Dispatcher = new SuCorrientazoDomicilioBussiness.SuCorrientazoDispatcher();

            Dispatcher.LoadDroneInformationToDispatch();

            Assert.AreEqual(1, Dispatcher.DroneManagerInstance.NumberOfDrones());

            Dispatcher.WriteDispatchedDroneInformation();

            FileInfo file = new FileInfo(System.IO.Path.Combine(Dispatcher.directoryWritten, "out01.txt"));
            Assert.AreEqual(true, file.Exists);
            Assert.AreNotEqual(file.Length, 0);
        }





    }
}
