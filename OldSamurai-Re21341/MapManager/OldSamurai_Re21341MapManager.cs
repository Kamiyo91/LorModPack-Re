using UnityEngine;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace OldSamurai_Re21341.MapManager
{
    public class OldSamurai_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "Reflection_Re21341.mp3" };
        public override void EnableMap(bool b)
        {
            sephirahColor = Color.cyan;
            base.EnableMap(b);
        }
    }
}