﻿using BLL_Re21341.Models;

namespace Raziel_Re21341.Passives
{
    public class PassiveAbility_InquisitorShimmering_Re21341 : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void OnRoundStartAfter()
        {
            owner.cardSlotDetail.RecoverPlayPoint(owner.cardSlotDetail.GetMaxPlayPoint());
            SetCards();
        }

        private void SetCards()
        {
            owner.allyCardDetail.ExhaustAllCards();
            AddNewCard(new LorId(ModParameters.PackageId, 52));
            AddNewCard(new LorId(ModParameters.PackageId, 52));
            AddNewCard(new LorId(ModParameters.PackageId, 52));
            AddNewCard(new LorId(ModParameters.PackageId, 53));
            AddNewCard(new LorId(ModParameters.PackageId, 53));
            AddNewCard(new LorId(ModParameters.PackageId, 54));
            AddNewCard(new LorId(ModParameters.PackageId, 55));
            AddNewCard(new LorId(ModParameters.PackageId, 55));
            AddNewCard(new LorId(ModParameters.PackageId, 55));
        }

        private void AddNewCard(LorId id)
        {
            var card = owner.allyCardDetail.AddTempCard(id);
            if (card.GetOriginCost() < 4)
                card?.SetCostToZero();
        }
    }
}