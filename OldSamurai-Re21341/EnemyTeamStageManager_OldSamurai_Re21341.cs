using CustomMapUtility;
using OldSamurai_Re21341.MapManager;

namespace OldSamurai_Re21341
{
    public class EnemyTeamStageManager_OldSamurai_Re21341 : EnemyTeamStageManager
    {
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("OldSamurai_Re21341", typeof(OldSamurai_Re21341MapManager), false, true,
                0.5f,
                0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _phaseChanged = false;
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }

        private void CheckPhase()
        {
            if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count > 0 || _phaseChanged) return;
            _phaseChanged = true;
            CustomMapHandler.SetMapBgm("Hornet_Re21341.ogg", true, "OldSamurai_Re21341");
        }
    }
}