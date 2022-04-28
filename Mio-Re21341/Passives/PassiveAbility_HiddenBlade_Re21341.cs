using BLL_Re21341.Models;
using KamiyoStaticUtil.Utils;

namespace Mio_Re21341.Passives
{
    public class PassiveAbility_HiddenBlade_Re21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 11, KamiyoModParameters.PackageId);
        }
    }
}