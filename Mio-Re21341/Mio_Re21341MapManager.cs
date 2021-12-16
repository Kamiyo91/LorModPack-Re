﻿using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Mio_Re21341
{
    public class Mio_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "MioPhase1_Re21341.mp3" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}