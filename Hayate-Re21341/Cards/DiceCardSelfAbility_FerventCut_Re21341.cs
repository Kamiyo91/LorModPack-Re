using KamiyoModPack.Hayate_Re21341.Buffs;
using LOR_DiceSystem;

namespace KamiyoModPack.Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_FerventCut_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
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
            var buf =
                owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_EntertainMe_Re21341) as
                    BattleUnitBuf_EntertainMe_Re21341;
            buf?.OnAddBuf(_atkLand < Check ? -1 : 1);
        }
    }
}