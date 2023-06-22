using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.Passives
{
    public class PassiveAbility_HighGuard_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.ReadyCounterCard(41, KamiyoModParameters.PackageId);
        }
    }
}