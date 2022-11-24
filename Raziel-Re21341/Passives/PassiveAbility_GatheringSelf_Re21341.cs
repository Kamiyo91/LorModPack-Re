using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Raziel_Re21341.Passives
{
    public class PassiveAbility_GatheringSelf_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 38, KamiyoModParameters.PackageId);
        }
    }
}