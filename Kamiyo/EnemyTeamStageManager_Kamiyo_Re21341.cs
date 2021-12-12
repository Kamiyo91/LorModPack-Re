using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using Kamiyo_Re21341.MapManager;
using Kamiyo_Re21341.Passives;
using UnityEngine;
using Util_Re21341;
using Util_Re21341.CustomMapUtility.Assemblies;

namespace Kamiyo_Re21341
{
    public class EnemyTeamStageManager_Kamiyo_Re21341 : EnemyTeamStageManager
    {
        private BattleUnitModel _mainEnemyModel;
        private bool _phaseChanged;
        private PassiveAbility_AlterEgoNpc_Re21341 _kamiyoEnemyPassive;

        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap("Kamiyo1_Re21341", new Kamiyo1_Re21341MapManager(), false, true, 0.5f, 0.2f,
                0.5f, 0.45f);
            CustomMapHandler.InitCustomMap("Kamiyo2_Re21341", new Kamiyo2_Re21341MapManager(), false, true, 0.5f,
                0.475f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            if (_mainEnemyModel != null)
                _kamiyoEnemyPassive =
                    _mainEnemyModel.passiveDetail.PassiveList.Find(x => x is PassiveAbility_AlterEgoNpc_Re21341) as
                        PassiveAbility_AlterEgoNpc_Re21341;
            if (Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>("Phase", out var curPhase))
                _phaseChanged = curPhase;
            if (!_phaseChanged) return;
            PrepareKamiyoUnit(false,true);
            PrepareMioEnemyUnit();
            CustomMapHandler.EnforceMap(1);
            Singleton<StageController>.Instance.CheckMapChange();
            UnitUtil.RefreshCombatUI();

        }
        public override void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("Phase", _phaseChanged);
            var list = new List<UnitBattleDataModel> { BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault()?.UnitData };
            currentWaveModel.ResetUnitBattleDataList(list);
        }
        public override void OnRoundEndTheLast() => CheckPhase();
        public override void OnRoundStart() => CustomMapHandler.EnforceMap(_phaseChanged ? 1 : 0);
        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 100 || _phaseChanged) return;
            _phaseChanged = true;
            PrepareKamiyoUnit(true);
            PrepareMioEnemyUnit();
        }

        private void PrepareKamiyoUnit(bool recoverHp,bool restart = false)
        {
            _kamiyoEnemyPassive.ForcedEgo();
            _kamiyoEnemyPassive.ActiveMassAttackCount();
            _kamiyoEnemyPassive.SetCountToMax();
            if(!restart)_mainEnemyModel.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 11));
            _mainEnemyModel.Book.SetHp(514);
            _mainEnemyModel.Book.SetBp(273);
            if(recoverHp)_mainEnemyModel.RecoverHP(514);
            _mainEnemyModel.breakDetail.ResetGauge();
            _mainEnemyModel.breakDetail.nextTurnBreak = false;
            _mainEnemyModel.breakDetail.RecoverBreakLife(1, true);
        }
        private static void PrepareMioEnemyUnit()
        {
            UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 5,
                EmotionLevel = 4,
                Pos = 1,
                OnWaveStart = true
            });
        }
    }
}
