using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UniversalInventorySystem.Classes;

namespace TrainRobberiesV
{
    internal class Mod
    {
        public static void VerifyFiles()
        {
            if (!File.Exists("scripts\\TrainRobberiesV.json"))
            {
                ModConfig newConfig = new ModConfig()
                {
                    debugMode = false,
                    cars = new List<FreightCar>()
                    {
                        new FreightCar()
                        {
                            modelName = "freightcont1",
                            radius = 2.5f
                        }
                    },
                    items = new List<Item>()
                    {
                        new Item
                        {
                            itemName = "Shipment of electronics",
                            itemValue = 5000
                        },
                        new Item
                        {
                            itemName = "Shipment of jewelry",
                            itemValue = 15000
                        },
                        new Item
                        {
                            itemName = "Food shipment",
                            itemValue = 0
                        },
                        new Item
                        {
                            itemName = "Weapons shipment",
                            itemValue = 1500
                        }
                    }
                };
                string json = JsonConvert.SerializeObject(newConfig, Formatting.Indented);
                File.WriteAllText("scripts\\TrainRobberiesV.json", json);
            }
        }

        public static void Log(string text)
        {
            File.WriteAllText("TrainRobberiesV.log", $"{File.ReadAllText("TrainRobberiesV.log")}\n{text}");
        }
    }
}
