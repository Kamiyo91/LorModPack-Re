using BigDLL4221.Utils;
using UnityEngine;

namespace KamiyoModPack.Kamiyo_Re21341.MapManager
{
    public class Kamiyo2_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "KamiyoPhase2_Re21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}