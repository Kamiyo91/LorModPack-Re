using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_HiddenBlade_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 11, KamiyoModParameters.PackageId);
        }
    }
}