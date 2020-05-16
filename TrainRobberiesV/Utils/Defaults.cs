using System.Collections.Generic;
using TrainRobberiesV.Utils.Classes;

namespace TrainRobberiesV
{
    internal class Defaults
    {
        public static List<TrainRobberiesV.Items.Item> defaultItems = new List<TrainRobberiesV.Items.Item>()
        {
            new TrainRobberiesV.Items.Item
            {
                name = "Shipment of electronics",
                value = 5000,
                //type = ItemType.Pawn
            },
            new TrainRobberiesV.Items.Item
            {
                name = "Shipment of jewelry",
                value = 15000,
                //type = ItemType.Pawn
            },
            new TrainRobberiesV.Items.Item
            {
                name = "Food shipment",
                value = 0,
                //type = ItemType.Junk
            },
            new TrainRobberiesV.Items.Item
            {
                name = "Weapons shipment",
                value = 1500,
                //type = ItemType.Pawn
            },
            new Items.Item
            {
                name = "E-Waste",
                value = 2500
            },
            new Items.Item
            {
                name = "Garbage",
                value = 0
            },
            new Items.Item
            {
                name = "Antiques",
                value = 500
            },
            new Items.Item
            {
                name = "Gold",
                value = 25000
            }
        };

        public static List<FreightCar> defaultCars = new List<FreightCar>()
        {
            new FreightCar()
            {
                modelName = "freightcont1",
                radius = 2.5f,
                bone = "bogie_r"
            },
            new FreightCar()
            {
                modelName = "freightcont2",
                radius = 2.5f,
                bone = "bogie_r"
            }
        };

        public static List<Fence> defaultFences = new List<Fence>()
        {
            new Fence()
            {
                name = "Paleto Fence",
                locX = 417.523f, 
                locY = 6520.681f, 
                locZ = 27.715f
            }, 
            new Fence()
            {
                name = "Desert Fence",
                locX = 470.829f,
                locY = 3552.115f,
                locZ = 33.239f
            },
            new Fence()
            {
                name = "Los Santos Fence",
                locX = 890.601f,
                locY = -2221.044f,
                locZ = 30.510f
            }
        };
    }
}
