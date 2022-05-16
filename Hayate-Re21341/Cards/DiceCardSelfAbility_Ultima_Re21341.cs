using Hayate_Re21341.Buffs;
using LOR_DiceSystem;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_Ultima_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 3;
        private int _atkLand;

        public override void OnUseCard()
        {
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            var buff =
                owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_EntertainMe_Re21341) as
                    BattleUnitBuf_EntertainMe_Re21341;
            buff?.AddStack(3);
        }
    }
}