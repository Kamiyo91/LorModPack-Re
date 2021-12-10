using BLL_Re21341.Models;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_KurosawaFireMagic_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc = "A";
        private int _defClashWin;
        private const int Check = 2;
        public override void OnUseCard()
        {
            _defClashWin = 0;
        }

        public override void OnWinParryingDef()
        {
            _defClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_defClashWin < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x => x != card.card && x.GetID() == new LorId(ModParameters.PackageId,1)))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            owner.allyCardDetail.DrawCards(1);
        }
    }
}
