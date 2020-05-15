using System.Collections.Generic;
using TrainRobberiesV.Utils.Classes;

namespace TrainRobberiesV
{
    internal class ModConfig
    {
        public bool draw3dMarkers { get; set; } = true;
        public bool debugMode { get; set; }
        public List<Items.Item> items { get; set; }
        public List<Fence> fences { get; set; }
        public List<FreightCar> cars { get; set; }
    }
}
