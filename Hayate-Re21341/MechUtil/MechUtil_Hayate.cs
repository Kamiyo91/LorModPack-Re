using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Hayate_Re21341.MechUtil
{
    public class MechUtil_Hayate : MechUtilBase
    {
        private BattleUnitModel _fingersnapSpecialTarget;

        public MechUtil_Hayate(MechUtilBaseModel model) : base(model, KamiyoModParameters.PackageId)
        {
        }

        public override void ExtraMethodOnRoundEndTheLastIgnoreDead()
        {
            if (_fingersnapSpecialTarget == null) return;
            BattleObjectManager.instance.UnregisterUnit(_fingersnapSpecialTarget);
            _fingersnapSpecialTarget = null;
            UnitUtil.RefreshCombatUI();
        }

        public override void OnUseExpireCard(BattlePlayingCardDataInUnitModel card)
        {
            base.OnUseExpireCard(card);
            if (card.card.GetID() != new LorId(KamiyoModParameters.PackageId, 907)) return;
            _fingersnapSpecialTarget = card.target;
        }
    }
}