using LOR_DiceSystem;

namespace KamiyoModPack.Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_Rage_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPoint(1);
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }


        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }
}