using System.Linq;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_Loneliness_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (BattleObjectManager.instance.GetAliveList(owner.faction)
                    .Count(x => !x.passiveDetail.HasPassive<PassiveAbility_NotEquip_Re21341>()) == 1)
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 3);
        }
    }
}