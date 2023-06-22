using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Raziel_Re21341.Passives
{
    public class PassiveAbility_GatheringSelf_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.ReadyCounterCard(38, KamiyoModParameters.PackageId);
        }
    }
}