namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmunityToNull_Re21341 : BattleUnitBuf
    {
        public override bool IsImmune(KeywordBuf buf)
        {
            return buf == KeywordBuf.NullifyPower;
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}