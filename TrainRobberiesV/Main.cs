using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using Newtonsoft.Json;
using TrainRobberiesV.Items;

namespace TrainRobberiesV
{
    public class Main : Script
    {
        private ModConfig config = new ModConfig();

        private List<Entity> robbedCars = new List<Entity>();

        public Main()
        {
            Mod.VerifyFiles();
            config = LoadConfig();
            Tick += OnTick;
            UI.Notify("~y~Train Robberies V ~w~started successfully!");
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!Game.Player.Character.IsInVehicle())
            {
                Vehicle[] vehicles = World.GetNearbyVehicles(Game.Player.Character, 25.5f);
                foreach (Vehicle veh in vehicles)
                {
                    FreightCar car = null;
                    foreach (FreightCar fc in config.cars) if (new Model(fc.modelName) == veh.Model) car = fc;
                    if (car != null && !robbedCars.Contains(veh) /*&& veh.IsAlive*/)
                    {
                        if (veh.HasBone(car.bone))
                        {
                            Vector3 rearPos = veh.GetBoneCoord(veh.GetBoneIndex(car.bone));
                            if (World.GetDistance(rearPos, Game.Player.Character.Position) <= car.radius)
                            {
                                // The player can rob the train
                                UI.ShowHelpMessage("Press ~y~E ~w~to rob the train", 1, true);

                                if (Game.IsControlJustPressed(0, GTA.Control.Talk))
                                {
                                    // Fading effect when robbing
                                    Game.FadeScreenOut(1500);
                                    Wait(3000);
                                    SearchTrain(veh);
                                    Game.FadeScreenIn(1500);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SearchTrain(Vehicle train)
        {
            try
            {
                robbedCars.Add(train);
                Random r = new Random();
                string lootText = "";

                // Get a random item and give it to the user if it's worth anything
                var item = config.items[r.Next(0, config.items.Count)];

                // Determine what the lootText should be
                if (((PawnItem)item).type == ItemType.Pawn) lootText = $"{item.itemName} (${item.itemValue})";
                else lootText = item.itemName;

                UI.Notify($"Train looted: {lootText}");
                if (item.itemValue > 0)
                {
                    // Give the user the item
                    // TODO: IMPLEMENT
                }
            }
            catch(Exception ex)
            {
                UI.Notify("Search failed!");
                Mod.Log($"Failed Search: \n{ex.Message}\n{ex.StackTrace}\n{ex.Source}\n{ex.StackTrace}\n\n\n\n\n");
            }
            
        }
        private ModConfig LoadConfig()
        {
            // Read and deserialze the mod configuration
            string json = File.ReadAllText("scripts\\TrainRobberiesV.json");
            return JsonConvert.DeserializeObject<ModConfig>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
