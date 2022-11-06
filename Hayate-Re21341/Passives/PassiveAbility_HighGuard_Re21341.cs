using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_HighGuard_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 41, KamiyoModParameters.PackageId);
        }
    }
}