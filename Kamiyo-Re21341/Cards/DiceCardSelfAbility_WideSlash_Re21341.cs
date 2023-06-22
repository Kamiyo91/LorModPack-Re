namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_WideSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkClashWin;
        private int _stacks;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkClashWin = 0;
            _stacks = 1;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, _stacks, owner);
            owner.RecoverHP(2);
        }
    }
}