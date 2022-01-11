using CustomMapUtility;
using UnityEngine;

namespace Raziel_Re21341
{
    public class Raziel_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "RazielPhase1_Re21341.mp3" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}