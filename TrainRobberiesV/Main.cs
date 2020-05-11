using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using GTA.Math;
using GTA.Native;

namespace TrainRobberiesV
{
    public class Main : Script
    {
        public Main()
        {
            UI.Notify("Train Robberies V started successfully!");
            Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            
        }
    }
}
