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
        private readonly bool _finalMech;
        private readonly NpcMechUtil_HayateModel _model;
        private bool _singleUseMechCard;

        public NpcMechUtil_Hayate(NpcMechUtil_HayateModel model) : base(model)
        {
            _model = model;
            _buf = new BattleUnitBuf_EntertainMe_Re21341();
            model.Owner.bufListDetail.AddBufWithoutDuplication(_buf);
            _finalMech = Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id == 4;
            _singleUseMechCard = false;
        }

        public override void ForcedEgo()
        {
            base.ForcedEgo();
            _buf.stack = 40;
        }

        public override void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (_model.FinalMechStart && !_model.OneTurnCard)
            {
                if (_finalMech)
                {
                    _model.DrawBack = _model.Owner.allyCardDetail.GetHand().Count;
                    _model.Owner.allyCardDetail.DiscardACardByAbility(_model.Owner.allyCardDetail.GetHand());
                    origin = BattleDiceCardModel.CreatePlayingCard(
                        ItemXmlDataList.instance.GetCardItem(_model.SecondaryMechCard));
                }
                else if(!_singleUseMechCard)
                {
                    _singleUseMechCard = true;
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
                    x.UnitData.unitData.bookItem.ClassInfo.id != new LorId(ModParameters.PackageId, 10000004));
            if (BattleObjectManager.instance
                .GetAliveList(Faction.Player).Any(x => !x.UnitData.unitData.isSephirah))
                return RandomUtil.SelectOne(BattleObjectManager.instance.GetAliveList(Faction.Player)
                    .Where(x => !x.UnitData.unitData.isSephirah).ToList());
            return null;
        }
    }
}