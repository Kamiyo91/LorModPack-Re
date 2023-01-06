namespace KamiyoModPack.Hayate_Re21341.Buffs
{
    public class BattleUnitBuf_Serious_Re21341 : BattleUnitBuf
    {
        public BattleUnitBuf_Serious_Re21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "Serious_Re21341";
        protected override string keywordIconId => "Serious_Re21341";

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            behavior.card.target?.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1);
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -1;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }
    }
}