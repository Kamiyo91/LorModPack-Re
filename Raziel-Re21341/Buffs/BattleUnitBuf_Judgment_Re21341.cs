using BigDLL4221.Buffs;

namespace KamiyoModPack.Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_Judgment_Re21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        protected override string keywordId => "Judgment_Re21341";
        protected override string keywordIconId => "Judgment_Re21341";
        public override int MaxStack => 0;
        public override int paramInBufDesc => 0;

        public override int GetBreakDamageIncreaseRate()
        {
            return 5;
        }

        public override int GetDamageIncreaseRate()
        {
            return 5;
        }
    }
}