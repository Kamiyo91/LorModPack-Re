using System.Linq;
using BLL_Re21341.Models;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_LoneFixer_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(owner.faction).Count(x =>
                    !(x.Book.GetBookClassInfoId().packageId == ModParameters.PackageId &&
                      x.Book.GetBookClassInfoId().id == 10000900)) == 1)
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 3);
        }
    }
}