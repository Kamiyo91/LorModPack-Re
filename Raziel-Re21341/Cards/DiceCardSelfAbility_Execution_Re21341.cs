﻿using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;

namespace KamiyoModPack.Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_Execution_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 12;
        private int _atkLand;

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
            UnitUtil.ReadyCounterCard(owner, 56, KamiyoModParameters.PackageId);
        }
    }
}