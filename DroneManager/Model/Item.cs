using DroneManager.Enums;
using DroneManager.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Model
{
    /// <summary>
    /// Represents any Item the Dron can carry
    /// </summary>
    public class Item : IEquatable<Item>
    {

        /// <summary>
        /// this item must be unique
        /// this id is the id that represents the object to transport
        /// </summary>
        public long ItemId { get; private set; }
        /// <summary>
        /// Weight of the drome
        /// </summary>
        public float Weight { get; private set; }

        /// <summary>
        /// Type of the item to transport
        /// </summary>
        public ITemType ItemType { get; private set; }


        private Item()
        {

        }



        /// <summary>
        /// Factory method ,since u just want to create objects
        /// </summary>
        /// <param name="itemtype">should be a value diferent to none </param>
        /// <param name="weight">if it is lower than zero this value of the instance will be zero</param>
        /// <param name="itemId">should be an item bigger than zero</param>
        /// <returns></returns>
        public static Item CreateItem(long itemId, ITemType itemtype, float weight= 0)
        {

          
            if (itemId <= 0) {
                throw new InvalidItemCreation("Invalid id Item creation");
            }


            if (itemtype  == ITemType._none)
            {
                throw new InvalidItemCreation("Invalid itemtype Item creation");
            }



            if (weight < 0)
                weight = 0;


            return new Item()
            {
                Weight = weight,

                ItemType = itemtype,

                ItemId = itemId
            };
        }

        public override bool Equals(Object other)
        {
            // as is like a cast that returns null if the object is a type or subtype
            return Equals(other as Item);
        }


        public bool Equals(Item other)
        {
            return other != null &&
                 (other == this) ||
                 (
                    (other.ItemId == this.ItemId && other.ItemType == this.ItemType && other.Weight == this.Weight)
                 );
        }


        public override int GetHashCode()
        {

            int hash = 17;

            hash = hash * 23 + ItemId.GetHashCode();

            hash = hash * 23 + ItemType.GetHashCode();

            hash = hash * 23 + Weight.GetHashCode();

            return hash;

        }
    }
}
