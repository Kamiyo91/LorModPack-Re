using CustomMapUtility;
using UnityEngine;

namespace OldSamurai_Re21341.MapManager
{
    public class OldSamuraiPlayer_Re21341MapManager : CustomMapManager
    {
        protected override string[] CustomBGMs => new[] { "Hornet_Re21341.mp3" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.cyan;
            base.EnableMap(b);
        }
    }
}