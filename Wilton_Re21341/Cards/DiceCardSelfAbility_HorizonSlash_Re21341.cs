﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_HorizonSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 4;
        private BattleUnitModel _target;
        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }

        public override void AfterGiveDamage(int damage, BattleUnitModel target)
        {
            _target = target;
        }

        public override void OnEndBattle()
        {
            if (_target == null || _target.bufListDetail.GetActivatedBufList()
                    .Count(x => x.positiveType == BufPositiveType.Negative) < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            _target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding,1,owner);
            _target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Vulnerable, 1, owner);
        }
    }
}
