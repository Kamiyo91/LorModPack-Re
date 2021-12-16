﻿using BLL_Re21341.Models;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_CustomInstantIndexRelease_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.passiveDetail.HasPassive<PassiveAbility_250115>() ||
                owner.passiveDetail.HasPassiveInReady<PassiveAbility_250115>())
                owner.passiveDetail.DestroyPassive(this);
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 42));
        }
    }
}