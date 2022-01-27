using System.Linq;
using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Kamiyo_Re21341.MechUtil
{
    public class NpcMechUtil_Kamiyo : NpcMechUtilBase
    {
        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        private readonly NpcMechUtil_KamiyoModel _model;

        public NpcMechUtil_Kamiyo(NpcMechUtil_KamiyoModel model) : base(model)
        {
            _model = model;
        }

        public override void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (_model.OneTurnCard && origin.GetID() == new LorId(ModParameters.PackageId, 22))
                origin = BattleDiceCardModel.CreatePlayingCard(
                    ItemXmlDataList.instance.GetCardItem(new LorId(ModParameters.PackageId, RandomUtil.Range(20, 21))));
            base.OnSelectCardPutMassAttack(ref origin);
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseKamiyoRe21341", _model.PhaseChanged);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                .Where(x => x.Book.BookId != new LorId(ModParameters.PackageId, 5)).Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > 161 || _model.PhaseChanged) return;
            _model.PhaseChanged = true;
            PrepareKamiyoUnit(false, true, true, true);
            PrepareMioEnemyUnit();
        }

        public void CheckRestart()
        {
            if (!_model.Restart) return;
            _model.Restart = false;
            _model.HasMechOnHp = false;
            PrepareKamiyoUnit(true);
            PrepareMioEnemyUnit();
            UnitUtil.RefreshCombatUI();
        }

        private void PrepareKamiyoUnit(bool restart, bool recoverHp = false, bool changeEmotionLevel = false,
            bool addPassive = false)
        {
            if (addPassive) AddAdditionalPassive();
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
            if (restart)
            {
                TurnEgoAbDialogOff();
                ForcedEgo();
            }
            else
            {
                ForcedEgo();
                _model.Owner.Book.SetHp(514);
                _model.Owner.Book.SetBp(273);
            }

            var card = _model.Owner.allyCardDetail.GetAllDeck()
                .FirstOrDefault(x => x.GetID() == new LorId(ModParameters.PackageId, 21));
            _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(ModParameters.PackageId, 22));
            SetMassAttack(true);
            SetCounter(4);
            if (recoverHp) _model.Owner.RecoverHP(514);
            _model.Owner.breakDetail.ResetGauge();
            _model.Owner.breakDetail.nextTurnBreak = false;
            _model.Owner.breakDetail.RecoverBreakLife(1, true);
            if (!changeEmotionLevel) return;
            UnitUtil.LevelUpEmotion(_model.Owner, 4);
            _model.Owner.emotionDetail.Reset();
        }

        private void PrepareMioEnemyUnit()
        {
            BattleUnitModel mioUnit;
            if (_model.Owner.faction == Faction.Enemy)
                mioUnit = UnitUtil.AddNewUnitEnemySide(new UnitModel
                {
                    Id = 5,
                    EmotionLevel = 4,
                    Pos = 1,
                    OnWaveStart = true
                });
            else
                mioUnit = UnitUtil.AddNewUnitPlayerSide(_floor, new UnitModel
                {
                    Id = 5,
                    Name = ModParameters.NameTexts.FirstOrDefault(x => x.Key.Equals("5")).Value,
                    EmotionLevel = 4,
                    Pos = BattleObjectManager.instance.GetList(Faction.Player).Count,
                    Sephirah = _floor.Sephirah
                });

            UnitUtil.ChangeCardCostByValue(mioUnit, -2, 4);
        }
    }
}