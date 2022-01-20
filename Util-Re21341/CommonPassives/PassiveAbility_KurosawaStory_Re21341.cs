using BLL_Re21341.Models;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_KurosawaStory_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.Book.BookId.packageId != ModParameters.PackageId ||
                !owner.passiveDetail.HasPassive<PassiveAbility_10012>()) owner.passiveDetail.DestroyPassive(this);
        }
    }
}