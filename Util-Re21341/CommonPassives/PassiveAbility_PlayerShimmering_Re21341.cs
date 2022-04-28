using KamiyoStaticUtil.CommonBuffs;
using KamiyoStaticUtil.Utils;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_PlayerShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_KamiyoPlayerShimmeringBuf>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_KamiyoPlayerShimmeringBuf());
            UnitUtil.DrawUntilX(owner, 6);
        }
    }
}