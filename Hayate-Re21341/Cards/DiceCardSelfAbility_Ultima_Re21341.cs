using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hayate_Re21341.Buffs;
using LOR_DiceSystem;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_Ultima_Re21341 : DiceCardSelfAbilityBase
    {
        private int _atkLand;
        private const int Check = 3;
        public override void OnUseCard() => _atkLand = 0;

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
