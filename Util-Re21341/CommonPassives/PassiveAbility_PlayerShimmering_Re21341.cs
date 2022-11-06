using BigDLL4221.Buffs;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_PlayerShimmering_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_ChangeCardCost_DLL4221>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_ChangeCardCost_DLL4221());
        }
    }
}