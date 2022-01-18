using CustomMapUtility;
using UnityEngine;

namespace Kamiyo_Re21341.MapManager
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