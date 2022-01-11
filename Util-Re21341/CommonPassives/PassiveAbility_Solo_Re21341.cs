using BLL_Re21341.Models;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_Solo_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.personalEgoDetail.RemoveCard(new LorId(ModParameters.PackageId, 61));
            owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 61));
        }
    }
}