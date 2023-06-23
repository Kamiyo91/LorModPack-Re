using UtilLoader21341.Util;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_NotEquip_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            UnitUtil.ChangeLoneFixerPassive<PassiveAbility_LoneFixer_Re21341>(owner.faction);
        }
    }
}