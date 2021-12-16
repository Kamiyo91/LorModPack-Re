namespace Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_WaterBlade_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            var speedDiceResultValue = card.speedDiceResultValue;
            var target = card.target;
            var targetSlotOrder = card.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count) return;
            var speedDice = target.speedDiceResult[targetSlotOrder];
            if (speedDiceResultValue - speedDice.value < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}