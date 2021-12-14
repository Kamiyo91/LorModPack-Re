using Util_Re21341;

namespace Hayate_Re21341.Passives
{
    public class PassiveAbility_HighGuard_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 41);
        }
    }
}
