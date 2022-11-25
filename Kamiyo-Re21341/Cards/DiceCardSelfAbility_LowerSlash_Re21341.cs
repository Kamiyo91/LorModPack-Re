﻿using BigDLL4221.Extensions;
using KamiyoModPack.Kamiyo_Re21341.Buffs;

namespace KamiyoModPack.Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_LowerSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
        private int _atkClashWin;

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            var buff = owner.GetActiveBuff<BattleUnitBuf_Shock_Re21341>();
            if (buff == null)
            {
                buff = new BattleUnitBuf_Shock_Re21341();
                owner.bufListDetail.AddBuf(buff);
            }

            buff.OnAddBuf(3);
            _atkClashWin = 0;
        }

        public override void OnWinParryingAtk()
        {
            _atkClashWin++;
        }

        public override void OnEndBattle()
        {
            if (_atkClashWin < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            owner.cardSlotDetail.RecoverPlayPointByCard(1);
        }
    }
}