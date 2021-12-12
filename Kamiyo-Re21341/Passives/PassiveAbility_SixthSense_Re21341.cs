using Util_Re21341;

namespace Kamiyo_Re21341.Passives
{
    public class PassiveAbility_SixthSense_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 18);
        }
    }
}
