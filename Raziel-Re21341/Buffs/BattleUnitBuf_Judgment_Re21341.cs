namespace KamiyoModPack.Raziel_Re21341.Buffs
{
    public class BattleUnitBuf_Judgment_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_Judgment_Re21341()
        {
            stack = 0;
        }

        protected override string keywordId => "Judgment_Re21341";
        protected override string keywordIconId => "Judgment_Re21341";
        public override int paramInBufDesc => 0;

        public override int GetBreakDamageIncreaseRate()
        {
            return 5;
        }

        public override int GetDamageIncreaseRate()
        {
            return 5;
        }

        public override void OnRoundEnd()
        {
            _owner.bufListDetail.RemoveBuf(this);
        }
    }
}