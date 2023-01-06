﻿namespace KamiyoModPack.Wilton_Re21341.Cards
{
    public class DiceCardSelfAbility_ShockWave_Re21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 2;
        private bool _atkSuccess;

        public override void OnUseCard()
        {
            _atkSuccess = false;
            owner.allyCardDetail.DrawCards(1);
        }

        public override void OnSucceedAttack()
        {
            _atkSuccess = true;
        }

        public override void OnEndBattle()
        {
            if (!_atkSuccess || !card.target.bufListDetail.GetActivatedBufList()
                    .Exists(x => x.bufType == KeywordBuf.Vulnerable && x.stack >= Check)) return;
            owner.allyCardDetail.DrawCards(1);
        }
    }
}