namespace Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_GatheringSelfInquisitor_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 8;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Protection, 1, owner);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }

        public override void AfterGiveDamage(int damage, BattleUnitModel target)
        {
            _atkLand += damage;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}