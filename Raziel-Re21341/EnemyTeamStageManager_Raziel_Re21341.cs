using CustomMapUtility;

namespace Raziel_Re21341
{
    public class EnemyTeamStageManager_Raziel_Re21341 : EnemyTeamStageManager
    {
        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Raziel_Re21341", typeof(Raziel_Re21341MapManager), false, true, 0.5f,
                0.375f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }
    }
}