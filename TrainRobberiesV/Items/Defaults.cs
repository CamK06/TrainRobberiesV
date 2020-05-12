using System.Collections.Generic;
using UniversalInventorySystem.Classes;

namespace TrainRobberiesV.Items
{
    internal class Defaults
    {
        public static List<Item> defaultItems = new List<Item>()
        {
            new PawnItem
            {
                itemName = "Shipment of electronics",
                itemValue = 5000,
                type = ItemType.Pawn
            },
            new PawnItem
            {
                itemName = "Shipment of jewelry",
                itemValue = 15000,
                type = ItemType.Pawn
            },
            new JunkItem
            {
                itemName = "Food shipment",
                itemValue = 0,
                type = ItemType.Junk
            },
            new PawnItem
            {
                itemName = "Weapons shipment",
                itemValue = 1500,
                type = ItemType.Pawn
            }
        };

        public static List<FreightCar> defaultCars = new List<FreightCar>()
        {
            new FreightCar()
            {
                modelName = "freightcont1",
                radius = 2.5f,
                bone = "bogie_r"
            }
        };
    }
}
