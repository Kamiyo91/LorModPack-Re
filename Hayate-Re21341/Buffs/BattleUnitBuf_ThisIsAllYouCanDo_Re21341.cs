namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_ThisIsAllYouCanDo_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_ThisIsAllYouCanDo_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "ThisIsAllYouCanDo_Re21341";
        protected override string keywordIconId => "ThisIsAllYouCanDo_Re21341";

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
        }

        public override void OnRoundStart()
        {
            _owner.allyCardDetail.DrawCards(1);
        }
    }
}