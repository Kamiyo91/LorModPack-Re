using BLL_Re21341.Models;
using BLL_Re21341.Models.MechUtilModels;
using Hayate_Re21341.Buffs;
using Util_Re21341;
using Util_Re21341.BaseClass;

namespace Hayate_Re21341.MechUtil
{
    public class MechUtil_Hayate : MechUtilBase
    {
        private readonly BattleUnitBuf_EntertainMe_Re21341 _buf;
        private readonly MechUtilBaseModel _model;
        private BattleUnitModel _fingersnapSpecialTarget;

        public MechUtil_Hayate(MechUtilBaseModel model) : base(model)
        {
            _model = model;
            _buf = new BattleUnitBuf_EntertainMe_Re21341();
            model.Owner.bufListDetail.AddBufWithoutDuplication(_buf);
        }

        public override void OnUseExpireCard(LorId cardId)
        {
            if (cardId == new LorId(ModParameters.PackageId, 29))
                _buf.stack = 0;
            if (cardId == new LorId(ModParameters.PackageId, 907))
                _model.Owner.personalEgoDetail.RemoveCard(cardId);
            base.OnUseExpireCard(cardId);
        }

        public void DeleteTarget()
        {
            if (_fingersnapSpecialTarget == null) return;
            BattleObjectManager.instance.UnregisterUnit(_fingersnapSpecialTarget);
            _fingersnapSpecialTarget = null;
            UnitUtil.RefreshCombatUI();
        }

        public void OnUseCardResetCount(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID() != new LorId(ModParameters.PackageId, 907)) return;
            _model.Owner.personalEgoDetail.RemoveCard(curCard.card.GetID());
            _fingersnapSpecialTarget = curCard.target;
            _model.Owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
            _buf.stack = 0;
        }
    }
}