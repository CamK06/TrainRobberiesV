using GTA;
using GTA.Math;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TrainRobberiesV.Utils.Classes;

namespace TrainRobberiesV
{
    internal class Mod
    {
        public static List<Blip> blips = new List<Blip>();
        public static ModConfig config = new ModConfig();

        public static void VerifyFiles()
        {
            if (!File.Exists("scripts\\TrainRobberiesV.json"))
            {
                ModConfig newConfig = new ModConfig()
                {
                    debugMode = false,
                    cars = Defaults.defaultCars,
                    items = Defaults.defaultItems,
                    fences = Defaults.defaultFences
                };
                string json = JsonConvert.SerializeObject(newConfig, Formatting.Indented/*, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }*/);
                File.WriteAllText("scripts\\TrainRobberiesV.json", json);
            }
            if (!File.Exists("scripts\\TrainRobbery-Inventories.json"))
            {
                List<Inventory> nList = new List<Inventory>();
                string json = JsonConvert.SerializeObject(nList, Formatting.Indented);
                File.WriteAllText("scripts\\TrainRobbery-Inventories.json", json);
            }
        }

        public static void Log(string text)
        {
            File.WriteAllText("TrainRobberiesV.log", $"{File.ReadAllText("TrainRobberiesV.log")}\n{text}");
        }

        public static void CreateBlips()
        {
            // Iterate over each pawnshop and create a blip
            foreach (Fence shop in config.fences)
            {
                Blip newBlip = World.CreateBlip(new Vector3(shop.locX, shop.locY, shop.locZ));
                newBlip.IsShortRange = true;
                newBlip.Sprite = BlipSprite.Custody;
                newBlip.Name = "Fence";
                blips.Add(newBlip);
            }
        }

        public static void LoadConfig()
        {
            // Read and deserialze the mod configuration
            string json = File.ReadAllText("scripts\\TrainRobberiesV.json");
            config =  JsonConvert.DeserializeObject<ModConfig>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public static void OnAbort(object sender, EventArgs e)
        {
            foreach (Blip blp in blips) blp.Remove();
        }
    }
}
