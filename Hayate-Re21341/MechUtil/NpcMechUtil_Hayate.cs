using BLL_Re21341.Extensions.MechUtilModelExtensions;
using BLL_Re21341.Models.MechUtilModels;
using Hayate_Re21341.Buffs;
using Util_Re21341.BaseClass;

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

        public override void SurviveCheck(int dmg)
        {
            base.SurviveCheck(dmg);
            _model.FinalMechStart = true;
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
        public override void OnUseCardResetCount(LorId cardId)
        {
            if (_model.SecondaryMechCard != cardId && _model.LorIdEgoMassAttack != cardId) return;
            if (_model.SecondaryMechCard == cardId) _model.FinalMechStart = false;
            _buf.stack = 0;
        }
    }
}
