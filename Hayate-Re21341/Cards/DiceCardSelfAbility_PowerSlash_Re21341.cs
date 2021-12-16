using Hayate_Re21341.Buffs;
using LOR_DiceSystem;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_PowerSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_EntertainMe_Re21341).stack += 3;
        }
    }
}