using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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
