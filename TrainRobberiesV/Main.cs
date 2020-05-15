using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GTA;
using GTA.Math;
using GTA.Native;
using Newtonsoft.Json;
using TrainRobberiesV.Utils.Classes;

namespace TrainRobberiesV
{
    public class Main : Script
    {
        private List<Entity> robbedCars = new List<Entity>();

        public Main()
        {
            Mod.VerifyFiles();
            Mod.LoadConfig();
            Mod.CreateBlips();
            InventoryManager.LoadInventories();
            // Events
            Tick += OnTick;
            Aborted += Mod.OnAbort;
            UI.Notify("~y~Train Robberies V ~w~successfully loaded!");
        }

        private void OnTick(object sender, EventArgs e)
        {
            // 3D markers
            if (Mod.config.draw3dMarkers)
                foreach (Fence fence in Mod.config.fences)
                    if (World.GetDistance(Game.Player.Character.Position, new Vector3(fence.locX, fence.locY, fence.locZ)) <= 150) World.DrawMarker(MarkerType.VerticalCylinder, new Vector3(fence.locX, fence.locY, fence.locZ - fence.locZ / fence.locZ), Vector3.Zero, Vector3.Zero, new Vector3(1.5f, 1.5f, 0.5f), Color.Yellow);


            // Actual looting stuff
            if (!Game.Player.Character.IsInVehicle())
            {
                // LOOTING
                Vehicle[] vehicles = World.GetNearbyVehicles(Game.Player.Character, 25.5f);
                foreach (Vehicle veh in vehicles)
                {
                    FreightCar car = null;
                    foreach (FreightCar fc in Mod.config.cars) if (new Model(fc.modelName) == veh.Model) car = fc;
                    if (car != null && !robbedCars.Contains(veh) && veh.IsAlive)
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
                                    if (!Mod.config.debugMode)
                                    {
                                        Game.FadeScreenOut(1500);
                                        Wait(3000);
                                    }
                                    SearchTrain(veh);
                                    if(!Mod.config.debugMode) Game.FadeScreenIn(1500);
                                }
                            }
                        }
                    }

                }
                // END OF LOOTING

                // FENCES

                Inventory inventory = InventoryManager.GetInventory((PedHash)Game.Player.Character.Model.GetHashCode());
                foreach (Fence shop in Mod.config.fences)
                    if (World.GetDistance(Game.Player.Character.Position, new Vector3(shop.locX, shop.locY, shop.locZ)) <= 1.25f && inventory.items.Count >= 1)
                    {
                        UI.ShowSubtitle($"Press ~y~E ~w~to sell your loot to the fence (${inventory.totalValue})", 1);

                        if (Game.IsControlJustPressed(0, GTA.Control.Talk))
                        {
                            Game.FadeScreenOut(1000);
                            Game.Player.Character.FreezePosition = true;
                            Wait(1000);
                            int itemCount = inventory.items.Count;
                            int itemValue = inventory.totalValue;
                            inventory.items.Clear();
                            inventory.totalValue = 0;
                            InventoryManager.SaveInventories();
                            World.CurrentDayTime = new TimeSpan(World.CurrentDayTime.Hours + new Random().Next(1, itemCount), new Random().Next(1, 59), new Random().Next(1, 59));
                            Wait(500);
                            Game.Player.Character.FreezePosition = false;
                            Game.FadeScreenIn(1000);
                            Game.Player.Money = Game.Player.Money + itemValue;
                            if (itemCount > 1) UI.Notify($"You just sold {itemCount} items for ${itemValue}!");
                            else if (itemCount == 1) UI.Notify($"You just sold {itemCount} item for ${itemValue}!");
                        }
                    }

                // END OF FENCES
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
                var item = Mod.config.items[r.Next(0, Mod.config.items.Count)];

                // Determine what the lootText should be
                lootText = item.name;
                
                UI.Notify($"Train looted: {lootText}");
                if (item.value > 0)
                {
                    // Give the user the item
                    InventoryManager.GetInventory((PedHash)Game.Player.Character.Model.GetHashCode()).items.Add(item);
                    InventoryManager.GetInventory((PedHash)Game.Player.Character.Model.GetHashCode()).totalValue += item.value;
                    InventoryManager.SaveInventories();
                }
            }
            catch(Exception ex)
            {
                UI.Notify("Search failed!");
                Mod.Log($"Failed Search: \n{ex.Message}\n{ex.StackTrace}\n{ex.Source}\n{ex.StackTrace}\n\n\n\n\n");
            }
            
        }
    }
}
