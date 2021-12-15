using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Hayate_Re21341
{
    public class Hayate_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "HayatePhase1_Re21341.mp3" };

        public override void InitializeMap()
        {
            base.InitializeMap();
            sephirahColor = Color.yellow;
        }
    }
}
