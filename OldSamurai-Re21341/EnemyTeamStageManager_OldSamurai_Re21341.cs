using System.Linq;
using BLL_Re21341.Models.MechUtilModels;
using CustomMapUtility;
using OldSamurai_Re21341.Buffs;
using OldSamurai_Re21341.MapManager;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace OldSamurai_Re21341
{
    public class EnemyTeamStageManager_OldSamurai_Re21341 : EnemyTeamStageManager
    {
        private BattleUnitModel _mainEnemyModel;
        private MechUtilBase _mechUtil;
        private bool _phaseChanged;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("OldSamurai_Re21341", typeof(OldSamurai_Re21341MapManager), false, true,
                0.5f,
                0.2f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            _mechUtil = new MechUtilBase(new MechUtilBaseModel
            {
                Owner = _mainEnemyModel,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_OldSamuraiEgoNpc_Re21341)
            });
            _phaseChanged = false;
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
        }

        public override void OnRoundStart()
        {
            if (_mechUtil.EgoCheck()) _mechUtil.EgoActive();
            CustomMapHandler.EnforceMap();
        }

        private void CheckPhase()
        {
            if (BattleObjectManager.instance.GetAliveList(Faction.Enemy).Count > 0 || _phaseChanged) return;
            _phaseChanged = true;
            UnitUtil.UnitReviveAndRecovery(_mainEnemyModel, _mainEnemyModel.MaxHp, true);
            _mechUtil.ForcedEgo();
            CustomMapHandler.SetMapBgm("Hornet_Re21341.ogg", true, "OldSamurai_Re21341");
        }
    }
}