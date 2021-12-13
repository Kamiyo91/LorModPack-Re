using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hayate_Re21341.Buffs;
using LOR_DiceSystem;

namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_LightingBlade_Re21341 : DiceCardSelfAbilityBase
    {
        private int _atkLand;
        private const int Check = 8;
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            _atkLand = 0;
        }
        public override void AfterGiveDamage(int damage, BattleUnitModel target)
        {
            _atkLand += damage;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}
