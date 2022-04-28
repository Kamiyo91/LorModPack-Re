using BLL_Re21341.Models;
using KamiyoStaticUtil.Utils;

namespace Hayate_Re21341.Passives
{
    public class PassiveAbility_HighGuard_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 41, KamiyoModParameters.PackageId);
        }
    }
}