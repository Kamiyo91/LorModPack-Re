namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_OutragingFire_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkClashWin;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkClashWin = 0;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction == Faction.Player
                         ? Faction.Enemy
                         : Faction.Player))
                unit.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Burn, 2);
        }
    }
}