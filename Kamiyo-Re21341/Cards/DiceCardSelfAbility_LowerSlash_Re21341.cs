namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_LowerSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
        private int _atkClashWin;

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            _atkClashWin = 0;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            owner.allyCardDetail.DrawCards(1);
        }
    }
}