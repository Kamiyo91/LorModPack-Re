using BigDLL4221.Utils;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_Loneliness_Re21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (owner.passiveDetail.PassiveList.Exists(x => x.id == new LorId("SaeModSa21341.Mod", 3)))
                owner.passiveDetail.DestroyPassive(this);
        }

        public override void OnRoundEnd()
        {
            if (UnitUtil.SupportCharCheck(owner) < 2)
                owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 3);
        }
    }
}