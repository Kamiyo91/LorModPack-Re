using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_KurosawaStory_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.Book.BookId.packageId != KamiyoModParameters.PackageId &&
                (!owner.passiveDetail.HasPassive<PassiveAbility_10012>() ||
                 !owner.passiveDetail.PassiveList.Exists(x =>
                     !x.destroyed && x.id == new LorId("SephirahBundleSe21341.Mod", 27))))
                owner.passiveDetail.DestroyPassive(this);
        }
    }
}