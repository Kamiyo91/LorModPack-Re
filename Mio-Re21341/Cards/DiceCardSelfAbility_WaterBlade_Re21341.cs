﻿using BigDLL4221.Extensions;
using BigDLL4221.Utils;

namespace KamiyoModPack.Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_WaterBlade_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPointByCard(1);
            if (!UnitUtil.CheckTargetSpeedByCard(card, Check)) return;
            owner.ChangeSameCardsCost(card, -1);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, owner);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }
    }
}