using Newtonsoft.Json;
using System.IO;
using TrainRobberiesV.Items;

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
                    cars = Defaults.defaultCars,
                    items = Defaults.defaultItems
                };
                string json = JsonConvert.SerializeObject(newConfig, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                File.WriteAllText("scripts\\TrainRobberiesV.json", json);
            }
        }

        public static void Log(string text)
        {
            File.WriteAllText("TrainRobberiesV.log", $"{File.ReadAllText("TrainRobberiesV.log")}\n{text}");
        }
    }
}
