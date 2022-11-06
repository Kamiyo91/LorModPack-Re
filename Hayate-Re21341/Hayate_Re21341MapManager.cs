using BigDLL4221.Utils;
using UnityEngine;

namespace KamiyoModPack.Hayate_Re21341
{
    public class Hayate_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "HayatePhase1_Re21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}