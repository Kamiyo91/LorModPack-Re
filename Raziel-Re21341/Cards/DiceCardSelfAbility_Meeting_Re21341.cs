﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_Meeting_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 8;
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
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            owner.allyCardDetail.DrawCards(1);
        }
    }
}
