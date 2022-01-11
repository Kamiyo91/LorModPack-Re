using CustomMapUtility;
using UnityEngine;

namespace Hayate_Re21341
{
    public class Hayate_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "HayatePhase1_Re21341.mp3" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}