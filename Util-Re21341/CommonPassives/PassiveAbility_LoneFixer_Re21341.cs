using UtilLoader21341.Util;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_LoneFixer_Re21341 : PassiveAbilityBase
    {
        public override void OnCreated()
        {
            rare = Rarity.Uncommon;
            name = Singleton<PassiveDescXmlList>.Instance.GetName(230008);
            desc = Singleton<PassiveDescXmlList>.Instance.GetDesc(230008);
        }

        public override void OnWaveStart()
        {
            UnitUtil.ChangeLoneFixerPassive(owner.faction,this);
        }

        public override void OnRoundEnd()
        {
            if (UnitUtil.SupportCharCheck(owner) < 2)
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 3);
        }

        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            return buf.positiveType != BufPositiveType.Negative;
        }
    }
}
