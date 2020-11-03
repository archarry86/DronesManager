using DroneManager.Exceptions;
using DroneManager.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DroneManager.Model.Drone
{
    public class Drone:IEquatable<Drone>, IDroneIdentificator, IDeliveryContainer
    {

     
        /// <summary>
        /// Max items capacity
        /// </summary>
        public readonly int   _max_items_capacity = 0;

        /// <summary>
        /// Even though the test did not mention the weight capacity 
        /// I would like to add it 
        /// 
        /// If this property is equals or lower than zero 
        /// the drone did not validate this property
        /// </summary>
        public readonly double _max_weight_capacity = -1;

        /// <summary>
        /// Machine serial for the drone
        /// Unique Identificator for the drone
        /// 
        /// insteof string I would use a Serial class that validate the serial of the macihne
        /// 
        /// 
        /// pre: this id should be unique
        /// 
        /// 
        /// </summary>
        public readonly String _drone_id = "";

        /// <summary>
        /// Items the drone may carry
        /// </summary>
        private List<Item> _drone_items = null;

        public IDeliveryInformation deliveryInformation;


        /// <summary>
        /// Represents the current _weight_capacity
        /// </summary>
        public double _Items_weight = -1;

        public String DroneId =>this._drone_id;

        public int Max_Items_Capacity => this._max_items_capacity;

        public double Weight_Items_Capacity => this._max_weight_capacity;

        public Drone(

           String droneid,
           int  max_items_capacity
            )
        {

            if (string.IsNullOrEmpty(droneid) ||
                max_items_capacity <= 0
                ) {

                //This message should be  in a .lang file in order to manage multi culture
                throw new InvalidDroneCreationException("Invalid Parameters for the Dron Creation.");
            }



            _drone_id = droneid;
            _max_items_capacity = max_items_capacity;

            _drone_items = new List<Item>(_max_items_capacity);
        }

        public Drone(

        String droneid,
        int max_items_capacity,
        double max_weight_capacity
         ):this(droneid, max_items_capacity)
        {

            if(max_weight_capacity > 0)
            this._max_weight_capacity = max_weight_capacity;

        }



        /// <summary>
        /// this metod add an item to this drone 
        /// 
        /// pre: Since this method it is not Thread safe this method should be called by multiple Threads all the same time
        /// 
        /// pos: if the drone has not achivied its maximun capacity  in both number of items and weight capacity the item is added
        /// 
        /// if I were to create a Thread safe method i would use a lock instrction in .net
        /// or syncrhonized in java
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item) {

            if (_drone_items.Count >= _max_items_capacity) {

                //This message should be  in a .lang file in order to manage multi culture
                throw new MaxDroneCapacityOverflow("this Drone has achieved its maximun capacity.");

            }

            var new_weight_capacity = _Items_weight + item.Weight;

            ValidateNewWeightCapacity(new_weight_capacity);
            ValidateDuplicateItem(item);


            this._drone_items.Add(item);
            //I think I am avoiding the posibilily of a invalid state in terms of items and the capacity
            //perhaps it may be better to iterate to set this variable
            this._Items_weight = new_weight_capacity;
          
        }

        /// <summary>
        /// Set the items of the drome
        /// 
        /// I intended to create a method that coul be only call only in my namespace but I could not
        /// 
        /// This may clone eache item of the parameter
        /// </summary>
        /// <param name="drone_items"></param>
        internal void SetItems(IEnumerable<Item> drone_items) {

            this._drone_items = new List<Item>( drone_items.ToList());
        }



        /// <summary>
        /// if the index exists in the items collection the index is replace with the new paramters
        /// if the index has not beed added the item is added
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Setitem(int index , Item item)
        {


            if ( index >= _max_items_capacity) {

                throw new  IndexOutOfRangeException("Index Out Of Range among drone Items");
            }


            if (index < _drone_items.Count && _drone_items[index] != null)
            {

                //sub the Weight of the element to remove
                double new_weight_capacity = _Items_weight + item.Weight - _drone_items[index].Weight;

                ValidateNewWeightCapacity(new_weight_capacity);
                this.ValidateDuplicateItem(item);
               
                this._drone_items[index] = item;
                //I think I am avoiding the posibilily of a invalid state in terms of items and the capacity
                //perhaps it may be better to iterate to set this variable
                this._Items_weight = new_weight_capacity;

            }
            else {
                //add the item if the index does not exists 

                this.AddItem(item);
            }

        }


        private void ValidateDuplicateItem(Item item) {

            if (this._drone_items.Contains(item))
            {
                throw new DuplicateDroneItem("the item has been already added to the drone.");

            }
        }



        private void ValidateNewWeightCapacity(double new_weight_capacity)
        {

           
            if (_max_weight_capacity > 0 && _max_weight_capacity > new_weight_capacity)
            {
                //This message should be  in a .lang file in order to manage multi culture
                throw new MaxWeightCapacityDroneException("this Drone has achieved its maximun capacity.",(new_weight_capacity- _max_weight_capacity));
            }

        }

        public override bool Equals(object other)
        {
            return Equals(other as Drone);
        }

        /// <summary>
        /// I intend to manage the only dron by its id 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Drone other)
        {
            return other != null && (String.Equals(other._drone_id, this._drone_id, StringComparison.InvariantCultureIgnoreCase));
        }



        public CoordinateLetter2D[] itemCoordinatesInformation(int index) {

            if (index<0 || index >= _max_items_capacity)
            {

                //This message should be  in a .lang file in order to manage multi culture
                throw new ArgumentException("This index is bigger than the drone capacity or lower than zero.");

            }

            if (index >= deliveryInformation.Positions.Length)
            {

                //This message should be  in a .lang file in order to manage multi culture
                throw new ArgumentException("This index is bigger than the drone capacity.");

            }


            return deliveryInformation.Positions[index];


        }

    }

    /// <summary>
    /// Since the dron constructor does not contains a parameter a IDeliveryInformation parameters
    /// I could have either create other constructor or implement a buider pattern to avoid creating multiple consutrctors
    /// 
    /// Also Builder pattern allows me to validate other scnearios I have not realized like 
    /// the items cuantity should be equals to DeliveryInformation coordiantes
    /// 
    /// I would say I could have associate the delivery information to the Item, but in the plain text there is no inforamtion 
    /// about the item.Still if I may assume all the items are Lunch in the Enumeration ITemType
    /// </summary>
    public class DroneBuilder {

       public String droneid { get; set; }
       public int max_items_capacity { get; set; }
       public double max_weight_capacity { get; set; }
       public DeliveryInformation deliveryInformation { get; set; }

        public List<Item> _drone_items = null;
        public Drone Build() {

        
           Drone drone = new Drone(droneid, max_items_capacity, max_weight_capacity);



            if (this.deliveryInformation != null && this._drone_items != null) {


                

                if (this.deliveryInformation.Positions.Length != this._drone_items.Count) {

                    throw new InvalidDroneCreationException("Delivery Information and drone_items do not mathc");
                }

                drone.SetItems(_drone_items);


            }


            return drone;
        
        }


    }
}
