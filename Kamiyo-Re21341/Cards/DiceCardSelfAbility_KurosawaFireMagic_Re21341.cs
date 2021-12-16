namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_KurosawaFireMagic_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
        private int _defClashWin;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _defClashWin = 0;
        }

        public override void OnWinParryingDef()
        {
            _defClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_defClashWin < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
        }
    }
}