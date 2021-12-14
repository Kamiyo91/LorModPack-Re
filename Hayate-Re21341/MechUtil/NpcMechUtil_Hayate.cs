using System.Linq;
using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models.MechUtilModels;
using Hayate_Re21341.Buffs;
using UnityEngine;
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
        public override void ForcedEgo()
        {
            base.ForcedEgo();
            _buf.stack = 40;
        }
        public override void OnSelectCardPutMassAttack(ref BattleDiceCardModel origin)
        {
            if (_model.FinalMechStart && !_model.OneTurnCard)
            {
                _model.DrawBack = _model.Owner.allyCardDetail.GetHand().Count;
                _model.Owner.allyCardDetail.DiscardACardByAbility(_model.Owner.allyCardDetail.GetHand());
                origin = BattleDiceCardModel.CreatePlayingCard(
                    ItemXmlDataList.instance.GetCardItem(_model.SecondaryMechCard));
                SetOneTurnCard(true);
                return;
            }
            if (!_model.MassAttackStartCount || _buf.stack < 40 || _model.OneTurnCard)
                return;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(_model.LorIdEgoMassAttack));
            SetOneTurnCard(true);
        }
        public override void OnUseCardResetCount(BattleDiceCardModel card)
        {
            if (_model.SecondaryMechCard != card.GetID() && _model.LorIdEgoMassAttack != card.GetID()) return;
            if (_model.SecondaryMechCard == card.GetID())
            {
                _model.FinalMechStart = false;
                _model.Owner.allyCardDetail.DrawCards(_model.DrawBack);
                _model.DrawBack = 0;
            }
            _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
            _buf.stack = 0;
        }
        public void SecondMechHpCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.SecondMechHp || !_model.SecondMechHpExist) return;
            _model.SecondMechHpExist = false;
            _model.FinalMechStart = true;
            UnitUtil.UnitReviveAndRecovery(_model.Owner,0);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ImmortalUntilRoundEnd_Re21341());
            _model.Owner.SetHp(_model.SecondMechHp);
        }
        public override void ExhaustEgoAttackCards()
        {
            var cards = _model.Owner.allyCardDetail.GetAllDeck().Where(x => x.GetID() == _model.LorIdEgoMassAttack || x.GetID() == _model.SecondaryMechCard);
            foreach (var card in cards)
            {
                _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
            }
        }
    }
}
