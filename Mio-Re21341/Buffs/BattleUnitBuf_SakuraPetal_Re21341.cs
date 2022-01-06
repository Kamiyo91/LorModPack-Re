namespace Mio_Re21341.Buffs
{
    public class BattleUnitBuf_SakuraPetal_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_SakuraPetal_Re21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "SakuraPetal_Re21341";
        protected override string keywordIconId => "SakuraPetal_Re21341";

        public override StatBonus GetStatBonus()
        {
            return new StatBonus
            {
                dmgAdder = -1
            };
        }
    }
}