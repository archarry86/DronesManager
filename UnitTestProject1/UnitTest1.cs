using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DroneManager.Classes;
using DroneManager.Enums;
using DroneManager.Exceptions;
using DroneManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuCorrientazoDomicilioBussiness.File.DataAccess;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        private Vector2dInt firstDelivery = new Vector2dInt(-2, 4);
        private Vector2dInt secondDelivery = new Vector2dInt(-1, 3);//(-1, -1);
        private Vector2dInt thirdDelivery = new Vector2dInt(0, 0);//-1, 3);

        private CoordinateLetter2D[][] coordinates;

        private AbstractDroneManager abstractDroneManager;

        private List<Guid> dronesGui = new List<Guid>();

        [TestMethod]
        public void TestVectorRotations()
        {
            Vector2dInt vector = Vector2dInt.Right;

            Vector2dInt rotation = vector.Rotate(90);

            Assert.AreEqual( Vector2dInt.Up, rotation);

            rotation = rotation.Rotate(90);

            Assert.AreEqual( Vector2dInt.Left, rotation);

            rotation = rotation.Rotate(90);

            Assert.AreEqual( Vector2dInt.Down, rotation);

            rotation = rotation.Rotate(90);

            Assert.AreEqual(Vector2dInt.Right, rotation);

        }





        [TestMethod]
        public void ValidateVectorFromAngle()
        {

            var result = Vector2dInt.VectorFromAngle(0);

            Assert.AreEqual( Vector2dInt.Right, result);
            result = Vector2dInt.VectorFromAngle(90);
            Assert.AreEqual( Vector2dInt.Up, result);
            result = Vector2dInt.VectorFromAngle(180);
            Assert.AreEqual( Vector2dInt.Left, result);
            result = Vector2dInt.VectorFromAngle(270);
            Assert.AreEqual( Vector2dInt.Down, result);
        }

        [TestMethod]
        public void TestNormalCalculation()
        {
            Vector2dInt vector = Vector2dInt.Right;

            Vector2dInt normal = vector.CalculateNormal();

            Assert.AreEqual(Vector2dInt.Up, normal);

            normal = normal.CalculateNormal();

            Assert.AreEqual( Vector2dInt.Left, normal);

            normal = normal.CalculateNormal();

            Assert.AreEqual( Vector2dInt.Down, normal);

            normal = normal.CalculateNormal();

            Assert.AreEqual( Vector2dInt.Right, normal);

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
            Assert.AreEqual( firstDelivery, lastposition.Position);

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
            Assert.AreEqual(  secondDelivery, lastposition.Position);

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

            Assert.AreEqual(thirdDelivery, lastposition.Position);

            index++;
            this.coordinates[index] = list.ToArray();

            Assert.AreEqual(false , this.coordinates.Any(p => p == null || p.Any(sp => sp == null)));


        }

        private CoordinateLetter2D[] CreateList(LeterCoordinates[] coordinates, CoordinateLetter2D lastcoordinate = null)
        {
            CoordinateLetter2D[] list = new CoordinateLetter2D[coordinates.Length];

            int i = 0;
            foreach (var letter in coordinates)
            {

                var newCoorinate = new CoordinateLetter2D(letter, lastcoordinate);
                list[i] = newCoorinate;
                i++;
                //parameter reasingnation but I am no generating any crazy side effect
                lastcoordinate = newCoorinate;



            }

            return list;
        }


        [TestMethod]
        public void CreateDefaultManager()
        {
            if (abstractDroneManager == null)
            {
                abstractDroneManager = new DefaultDroneManager();


                Assert.AreNotEqual( null , abstractDroneManager.NumberOfDrones());

            }
        }



        [TestMethod]
        public void CreateDrones()
        {
            CreateDefaultManager();



            int number_of_drones = 10;

            for (int index = 1; index <= number_of_drones; index++)
            {
                abstractDroneManager.CreateDrone(index.ToString());
            }

            Assert.AreEqual( number_of_drones, abstractDroneManager.NumberOfDrones());


            int number_of_drones_by_guid = 10;

            for (int index = 1; index <= number_of_drones_by_guid; index++)
            {
                Guid id = Guid.NewGuid();
                abstractDroneManager.CreateDrone(id.ToString());
                dronesGui.Add(id);
            }

            Assert.AreEqual(number_of_drones + number_of_drones_by_guid, abstractDroneManager.NumberOfDrones());



        }


        private SuCorrientazoDomicilioBussiness.SuCorrientazoDispatcher Dispatcher;

        [TestMethod]
        public void ValdiateGetItemRoute()
        {
            if (Dispatcher == null)
            {
                this.ValidateDispatcher();

            }

            var serial = "01";
           
            (var route, var item) = Dispatcher.DroneManagerInstance.GetItemRoute(serial, 0);

            Assert.AreNotEqual(null, route);
            Assert.AreNotEqual(null, item);


            Assert.ThrowsException<IndexOutOfRangeException>(() => Dispatcher.DroneManagerInstance.GetItemRoute(serial, 20));


        }

        [TestMethod]
        public void AddItemstoADroneSerialOne()
        {

            if (abstractDroneManager == null || abstractDroneManager.NumberOfDrones() == 0)
                CreateDrones();

            long index = 0;
            index++;
            Item it = Item.CreateItem(index, ITemType.Lunch, new Random().Next(1, 5));

            String serialone = "1";

            abstractDroneManager.AddItemToaDrone(it, serialone);

            //validate that i can not add a duplicate ittem to a drone
            Assert.ThrowsException<DuplicateDroneItem>(() => abstractDroneManager.AddItemToaDrone(it, serialone));

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
            if (this.coordinates == null)
                this.ValidateDispatcher();

            CoordinateLetter2DReader reader = new CoordinateLetter2DReader();
            var line = "AAAAIAA";

            var result = reader.read(line, 0);

            Assert.AreEqual( new Vector2dInt(-2, 4), result.Last().Position);


        }


        [TestMethod]
        public void ValidateDispatcher()
        {
            if (this.coordinates == null)
                this.ValidateRouteCalulation();


            Dispatcher = new SuCorrientazoDomicilioBussiness.SuCorrientazoDispatcher();

            Dispatcher.LoadDroneInformationToDispatch();

            var drone = Dispatcher.DroneManagerInstance.FindDrone("01");

            Assert.AreEqual(1, Dispatcher.DroneManagerInstance.NumberOfDrones());

            Dispatcher.WriteDispatchedDroneInformation();

            var path = Path.Combine(FileManager.Classes.FileManager.MyDirectoryFilesResult, drone.DeliveryInformation.ExecutionDate.ToString("yyyyMMdd"));


            FileInfo file = new FileInfo(System.IO.Path.Combine(path, "out01.txt"));
            Assert.AreEqual(true, file.Exists);
            Assert.AreNotEqual( 0, file.Length);
        }





    }
}
