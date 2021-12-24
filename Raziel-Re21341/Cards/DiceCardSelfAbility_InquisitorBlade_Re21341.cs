namespace Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_InquisitorBlade_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 12;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
        }

        public override void AfterGiveDamage(int damage, BattleUnitModel target)
        {
            _atkLand += damage;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, owner);
        }
    }
}