using CustomMapUtility;

namespace Raziel_Re21341
{
    public class EnemyTeamStageManager_Raziel_Re21341 : EnemyTeamStageManager
    {
        private int _count;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Raziel_Re21341", typeof(Raziel_Re21341MapManager), false, true, 0.5f,
                0.375f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _count = 0;
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
            _count++;
        }

        private void CheckPhase()
        {
            if (_count == 2)
                CustomMapHandler.SetMapBgm("RazielPhase2_Re21341.ogg", true, "Raziel_Re21341");
        }
    }
}