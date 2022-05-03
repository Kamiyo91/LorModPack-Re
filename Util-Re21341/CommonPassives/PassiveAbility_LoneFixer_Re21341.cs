using KamiyoStaticUtil.Utils;

namespace Util_Re21341.CommonPassives
{
    public class PassiveAbility_LoneFixer_Re21341 : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            if (UnitUtil.SupportCharCheck(owner) == 1)
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 3);
        }
    }
}