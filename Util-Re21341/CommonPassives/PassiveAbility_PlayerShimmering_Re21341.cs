using Util_Re21341.CommonBuffs;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_PlayerShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_PlayerShimmeringBuf_Re21341>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_PlayerShimmeringBuf_Re21341());
            UnitUtil.DrawUntilX(owner, 6);
        }
    }
}