using KamiyoModPack.BLL_Re21341.Models;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.Passives
{
    public class PassiveAbility_SixthSense_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.ReadyCounterCard(18, KamiyoModParameters.PackageId);
        }
    }
}