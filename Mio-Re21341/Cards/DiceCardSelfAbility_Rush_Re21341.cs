﻿using BigDLL4221.Extensions;
using BigDLL4221.Utils;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_Rush_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;

        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            if (!UnitUtil.CheckTargetSpeedByCard(card, Check)) return;
            owner.ChangeSameCardsCost(card, -1);
        }
    }
}