using UtilLoader21341.Extensions;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_PlayerShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_ChangeCardCost_DLL21341>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ChangeCardCost_DLL21341());
        }
    }
}