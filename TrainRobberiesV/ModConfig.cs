using System.Collections.Generic;
using UniversalInventorySystem.Classes;

namespace TrainRobberiesV
{
    internal class ModConfig
    {
        public bool debugMode { get; set; }
        public List<Item> items { get; set; }
        public List<FreightCar> cars { get; set; }
    }
}
