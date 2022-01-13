using System.Collections.Generic;
using System.Linq;
using BLL_Re21341.Models;
using CustomMapUtility;
using Kamiyo_Re21341.MapManager;
using Kamiyo_Re21341.Passives;
using Util_Re21341;

namespace Kamiyo_Re21341
{
    public class EnemyTeamStageManager_Kamiyo_Re21341 : EnemyTeamStageManager
    {
        private PassiveAbility_AlterEgoNpc_Re21341 _kamiyoEnemyPassive;
        private BattleUnitModel _mainEnemyModel;
        private bool _phaseChanged;
        private bool _restart;

        public override void OnWaveStart()
        {
            _phaseChanged = Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>("Phase", out var curPhase) && curPhase;
            _restart = _phaseChanged;
            CustomMapHandler.InitCustomMap("Kamiyo1_Re21341", typeof(Kamiyo1_Re21341MapManager), false, true, 0.5f,
                0.2f,
                0.5f, 0.45f);
            CustomMapHandler.InitCustomMap("Kamiyo2_Re21341", typeof(Kamiyo2_Re21341MapManager), false, true, 0.5f,
                0.475f, 0.5f, 0.225f);
            CustomMapHandler.EnforceMap(_phaseChanged ? 1 : 0);
            Singleton<StageController>.Instance.CheckMapChange();
            _mainEnemyModel = BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault();
            if (_mainEnemyModel != null)
                _kamiyoEnemyPassive =
                    _mainEnemyModel.passiveDetail.PassiveList.Find(x => x is PassiveAbility_AlterEgoNpc_Re21341) as
                        PassiveAbility_AlterEgoNpc_Re21341;
            if (!_phaseChanged) return;
            _mainEnemyModel?.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 11));
        }

        public override void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("Phase", _phaseChanged);
            var list = new List<UnitBattleDataModel>
                { BattleObjectManager.instance.GetList(Faction.Enemy).FirstOrDefault()?.UnitData };
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public override void OnRoundEndTheLast()
        {
            CheckPhase();
        }

        public override void OnRoundStart()
        {
            CheckRestart();
            CustomMapHandler.EnforceMap(_phaseChanged ? 1 : 0);
        }

        private void CheckPhase()
        {
            if (_mainEnemyModel.hp > 100 || _phaseChanged) return;
            _phaseChanged = true;
            CustomMapHandler.EnforceMap(1);
            Singleton<StageController>.Instance.CheckMapChange();
            PrepareKamiyoUnit(false, true, true, true);
            PrepareMioEnemyUnit();
        }

        private void CheckRestart()
        {
            if (!_restart) return;
            _restart = false;
            PrepareKamiyoUnit(true);
            PrepareMioEnemyUnit();
            UnitUtil.RefreshCombatUI();
        }

        private void PrepareKamiyoUnit(bool restart, bool recoverHp = false, bool changeEmotionLevel = false,
            bool addPassive = false)
        {
            if (addPassive) _kamiyoEnemyPassive.AddAdditionalPassive();
            UnitUtil.ChangeCardCostByValue(_mainEnemyModel, -2, 4);
            if (restart)
            {
                _kamiyoEnemyPassive.ForcedEgoRestart();
            }
            else
            {
                _kamiyoEnemyPassive.ForcedEgo();
                _mainEnemyModel.Book.SetHp(514);
                _mainEnemyModel.Book.SetBp(273);
            }

            var card = _mainEnemyModel.allyCardDetail.GetAllDeck()
                .FirstOrDefault(x => x.GetID() == new LorId(ModParameters.PackageId, 21));
            _mainEnemyModel.allyCardDetail.ExhaustACardAnywhere(card);
            _mainEnemyModel.allyCardDetail.AddNewCardToDeck(new LorId(ModParameters.PackageId, 22));
            _kamiyoEnemyPassive.ActiveMassAttackCount();
            _kamiyoEnemyPassive.SetCountToMax();
            if (recoverHp) _mainEnemyModel.RecoverHP(514);
            _mainEnemyModel.breakDetail.ResetGauge();
            _mainEnemyModel.breakDetail.nextTurnBreak = false;
            _mainEnemyModel.breakDetail.RecoverBreakLife(1, true);
            if (!changeEmotionLevel) return;
            _mainEnemyModel.emotionDetail.SetEmotionLevel(4);
            _mainEnemyModel.emotionDetail.Reset();
        }

        private static void PrepareMioEnemyUnit()
        {
            var mioUnit = UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 5,
                EmotionLevel = 4,
                Pos = 1,
                OnWaveStart = true
            });
            UnitUtil.ChangeCardCostByValue(mioUnit, -2, 4);
        }
    }
}