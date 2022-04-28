using BLL_Re21341.Models;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_Solo_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundStartAfter()
        {
            owner.personalEgoDetail.RemoveCard(new LorId(KamiyoModParameters.PackageId, 61));
            owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 61));
        }
    }
}