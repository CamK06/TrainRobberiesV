using GTA.Native;
using System.Collections.Generic;
using TrainRobberiesV.Items;

namespace TrainRobberiesV.Utils.Classes
{
    public class Inventory
    {
        public int totalValue { get; set; } = 0;
        public PedHash attachedPed { get; set; }
        public List<Item> items = new List<Item>();
    }
}
