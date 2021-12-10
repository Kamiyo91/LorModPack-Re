using BLL_Re21341.Models;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_OutragingFire_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc = "A";
        private int _atkClashWin;
        private const int Check = 2;
        public override void OnUseCard()
        {
            _atkClashWin = 0;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x => x != card.card && x.GetID() == new LorId(ModParameters.PackageId,1)))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            owner.cardSlotDetail.RecoverPlayPoint(2);
        }
    }
}
