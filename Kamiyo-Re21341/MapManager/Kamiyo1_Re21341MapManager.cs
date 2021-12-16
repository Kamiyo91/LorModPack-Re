using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Kamiyo_Re21341.MapManager
{
    public class Kamiyo1_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "KamiyoPhase1_Re21341.mp3" };

        public override void InitializeMap()
        {
            base.InitializeMap();
            sephirahColor = Color.black;
        }
    }
}