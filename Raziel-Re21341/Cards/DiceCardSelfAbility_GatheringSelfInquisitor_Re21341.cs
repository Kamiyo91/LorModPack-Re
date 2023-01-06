namespace KamiyoModPack.Raziel_Re21341.Cards
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
            owner.RecoverHP(3);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}