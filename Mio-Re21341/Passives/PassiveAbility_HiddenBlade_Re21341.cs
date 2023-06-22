using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.Passives
{
    public class PassiveAbility_HiddenBlade_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.ReadyCounterCard(11, KamiyoModParameters.PackageId);
        }
    }
}