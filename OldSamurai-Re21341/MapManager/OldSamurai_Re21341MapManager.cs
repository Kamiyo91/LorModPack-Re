using BigDLL4221.Utils;
using UnityEngine;

namespace KamiyoModPack.OldSamurai_Re21341.MapManager
{
    public class OldSamurai_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "Reflection_Re21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.cyan;
            base.EnableMap(b);
        }
    }
}