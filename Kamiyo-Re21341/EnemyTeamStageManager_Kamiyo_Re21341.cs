using System.Linq;
using CustomMapUtility;
using Kamiyo_Re21341.MapManager;

namespace Kamiyo_Re21341
{
    public class EnemyTeamStageManager_Kamiyo_Re21341 : EnemyTeamStageManager
    {
        private BattleUnitModel _mainEnemyModel;
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            _phaseChanged = Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>("Phase", out var curPhase) && curPhase;
            CustomMapHandler.InitCustomMap("Kamiyo1_Re21341", typeof(Kamiyo1_Re21341MapManager), false, true, 0.5f,
                0.2f, 0.5f, 0.45f);
            CustomMapHandler.InitCustomMap("Kamiyo2_Re21341", typeof(Kamiyo2_Re21341MapManager), false, true, 0.5f,
                0.475f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap(_phaseChanged ? 1 : 0);
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap(_phaseChanged ? 1 : 0);
        }

        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 161 || _phaseChanged) return;
            _phaseChanged = true;
            CustomMapHandler.EnforceMap(1);
            Singleton<StageController>.Instance.CheckMapChange();
        }
    }
}