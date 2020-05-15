using GTA.Native;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrainRobberiesV.Items;
using TrainRobberiesV.Utils.Classes;

namespace TrainRobberiesV
{
    public class InventoryManager
    {
        private static List<Inventory> inventories;

        public static void LoadInventories()
        {
            inventories = JsonConvert.DeserializeObject<List<Inventory>>(File.ReadAllText("scripts\\TrainRobbery-Inventories.json"));
        }

        public static Inventory GetInventory(PedHash ped)
        {
            // Try to get and return an inventory for the model
            Inventory inventory = inventories.FirstOrDefault(x => x.attachedPed == ped);
            if (inventory != null) return inventory;
            else
            {
                // Create a new inventory
                Inventory newInventory = new Inventory()
                {
                    attachedPed = ped,
                    items = new List<Item>()
                };

                // Add and return the inventory
                inventories.Add(newInventory);
                return newInventory;
            }
        }

        public static void SaveInventories()
        {
            File.WriteAllText("scripts\\TrainRobbery-Inventories.json", JsonConvert.SerializeObject(inventories, Formatting.Indented));
        }
    }
}
