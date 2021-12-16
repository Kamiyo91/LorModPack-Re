﻿using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Kamiyo_Re21341.MapManager
{
    public class Kamiyo2_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "KamiyoPhase2_Re21341.mp3" };
        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}