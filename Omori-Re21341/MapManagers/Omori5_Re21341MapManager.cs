using HarmonyLib;
using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Omori_Re21341.MapManagers
{
    public class Omori5_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "OmoriPhase1_Re21341.mp3" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}