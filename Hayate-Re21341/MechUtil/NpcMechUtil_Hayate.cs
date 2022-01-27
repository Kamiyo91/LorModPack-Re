using System.Linq;
using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models;
using Hayate_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.BaseClass;
using Util_Re21341.CommonBuffs;

namespace Hayate_Re21341.MechUtil
{
    public class NpcMechUtil_Hayate : NpcMechUtilBase
    {
        private readonly BattleUnitBuf_EntertainMe_Re21341 _buf;
        private readonly NpcMechUtil_HayateModel _model;

        public NpcMechUtil_Hayate(NpcMechUtil_HayateModel model) : base(model)
        {
            _model = model;
            _buf = new BattleUnitBuf_EntertainMe_Re21341();
            model.Owner.bufListDetail.AddBufWithoutDuplication(_buf);
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseHayateRe21341", _model.PhaseChanged);
            stageModel.SetStageStorgeData("PhaseFinalHayateRe21341", _model.SecondMechHpExist);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction).Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>("PhaseHayateRe21341", out var curPhase);
            if (Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<bool>("PhaseFinalHayateRe21341", out var curPhaseFinal))
                _model.SecondMechHpExist = curPhaseFinal;
            _model.PhaseChanged = curPhase;
            if (!_model.PhaseChanged) return;
            _model.HasMechOnHp = false;
            ForcedEgo();
            _model.Owner.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 44));
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
            _buf.stack = 1;
        }

        public override void ForcedEgo()
        {
            base.ForcedEgo();
            _buf.stack = 40;
        }

        public bool GetFinalMechValue()
        {
            return _model.FinalMech;
        }

        public override void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (_model.FinalMechStart && !_model.OneTurnCard)
            {
                if (_model.FinalMech)
                {
                    _model.DrawBack = _model.Owner.allyCardDetail.GetHand().Count;
                    _model.Owner.allyCardDetail.DiscardACardByAbility(_model.Owner.allyCardDetail.GetHand());
                    origin = BattleDiceCardModel.CreatePlayingCard(
                        ItemXmlDataList.instance.GetCardItem(_model.SecondaryMechCard));
                }
                else if (!_model.SingleUseMech)
                {
                    _model.SingleUseMech = true;
                    _buf.stack = 40;
                    origin = BattleDiceCardModel.CreatePlayingCard(
                        ItemXmlDataList.instance.GetCardItem(_model.LorIdEgoMassAttack));
                }

                SetOneTurnCard(true);
                return;
            }

            if (!_model.MassAttackStartCount || _buf.stack < 40 || _model.OneTurnCard)
                return;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(_model.LorIdEgoMassAttack));
            SetOneTurnCard(true);
        }

        public override void OnUseCardResetCount(BattlePlayingCardDataInUnitModel curCard)
        {
            if (_model.SecondaryMechCard != curCard.card.GetID() &&
                _model.LorIdEgoMassAttack != curCard.card.GetID()) return;
            if (_model.SecondaryMechCard == curCard.card.GetID())
            {
                _model.FinalMechStart = false;
                _model.MassAttackStartCount = false;
                _model.LastPhaseStart = true;
                _model.Owner.allyCardDetail.DrawCards(_model.DrawBack);
                _model.DrawBack = 0;
                _model.Owner.bufListDetail.RemoveBuf(_buf);
                _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_EntertainMeFinalPhase_Re21341());
                _model.Owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
                return;
            }

            _model.FingersnapTarget = curCard.target;
            _model.Owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
            _buf.stack = 0;
        }

        public void DeleteTarget()
        {
            if (_model.FingersnapTarget == null) return;
            BattleObjectManager.instance.UnregisterUnit(_model.FingersnapTarget);
            _model.FingersnapTarget = null;
            UnitUtil.RefreshCombatUI();
        }

        public void SecondMechHpCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.SecondMechHp || !_model.SecondMechHpExist) return;
            _model.SecondMechHpExist = false;
            _model.FinalMechStart = true;
            UnitUtil.UnitReviveAndRecovery(_model.Owner, 0, false);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEndMech_Re21341());
            _model.Owner.SetHp(_model.SecondMechHp);
            _model.Owner.breakDetail.ResetGauge();
            _model.Owner.breakDetail.RecoverBreakLife(1, true);
        }

        public override void ExhaustEgoAttackCards()
        {
            var cards = _model.Owner.allyCardDetail.GetAllDeck().Where(x =>
                x.GetID() == _model.LorIdEgoMassAttack || x.GetID() == _model.SecondaryMechCard);
            foreach (var card in cards) _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
        }

        public override BattleUnitModel ChooseEgoAttackTarget(LorId cardId)
        {
            if (cardId != _model.LorIdEgoMassAttack) return null;
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id ==
                new LorId(ModParameters.PackageId, 6))
                return BattleObjectManager.instance.GetAliveList(Faction.Player).FirstOrDefault(x =>
                    x.UnitData.unitData.bookItem.ClassInfo.id != new LorId(ModParameters.PackageId, 10000901));
            if (BattleObjectManager.instance
                .GetAliveList(Faction.Player).Any(x => !x.UnitData.unitData.isSephirah))
                return RandomUtil.SelectOne(BattleObjectManager.instance.GetAliveList(Faction.Player)
                    .Where(x => !x.UnitData.unitData.isSephirah).ToList());
            return null;
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > 527 || _model.PhaseChanged) return;
            _model.PhaseChanged = true;
            ForcedEgo();
            _model.Owner.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 44));
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 4);
        }

        public void HayateIsDeadBeforePhase3()
        {
            if (!_model.FinalMech) return;
            if (_model.LastPhaseStart) return;
            if (!_model.Owner.IsDead()) return;
            UnitUtil.UnitReviveAndRecovery(_model.Owner, 5, false);
        }
    }
}