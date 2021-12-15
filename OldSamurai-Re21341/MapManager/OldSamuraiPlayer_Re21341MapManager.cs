﻿using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace OldSamurai_Re21341.MapManager
{
    public class OldSamuraiPlayer_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "Hornet_Re21341.mp3" };
        public override void InitializeMap()
        {
            base.InitializeMap();
            sephirahColor = Color.cyan;
        }
    }
}
