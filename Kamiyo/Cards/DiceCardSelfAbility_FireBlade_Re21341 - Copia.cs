﻿using BLL_Re21341.Models;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_FireBlade_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc = "Can only be used at [Emotion Level 5] and [Alter Ego's Aura] is required";
        private int _atkClashWin;
        private int _check = 2;
        public override void OnUseCard()
        {
            _atkClashWin = 0;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < _check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck().FindAll(x => x != card.card && x.GetID() == new LorId(ModParameters.PackageId,1)))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }
            owner.allyCardDetail.DrawCards(1);
        }
    }
}
