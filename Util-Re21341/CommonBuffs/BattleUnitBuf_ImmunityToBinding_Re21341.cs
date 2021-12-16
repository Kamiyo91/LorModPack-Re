namespace Util_Re21341.CommonBuffs
{
    public class BattleUnitBuf_ImmunityToBinding_Re21341 : BattleUnitBuf
    {
        public override bool IsImmune(KeywordBuf buf)
        {
            return buf == KeywordBuf.Binding;
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}