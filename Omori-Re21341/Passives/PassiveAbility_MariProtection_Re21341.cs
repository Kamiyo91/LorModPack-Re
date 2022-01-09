using Util_Re21341;

namespace Omori_Re21341.Passives
{
    public class PassiveAbility_MariProtection_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 78);
        }
    }
}