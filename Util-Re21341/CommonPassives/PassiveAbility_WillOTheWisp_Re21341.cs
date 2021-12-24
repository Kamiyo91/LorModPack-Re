using System.Linq;
using Util_Re21341.CommonBuffs;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_WillOTheWisp_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(owner.faction)
                    .Count(x => x.passiveDetail.HasPassive<PassiveAbility_WillOTheWisp_Re21341>()) <= 2) return;
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, owner);
            owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, owner);
        }
    }
}