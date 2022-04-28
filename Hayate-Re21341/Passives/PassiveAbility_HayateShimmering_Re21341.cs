using BLL_Re21341.Models;

namespace Hayate_Re21341.Passives
{
    public class PassiveAbility_HayateShimmering_Re21341 : PassiveAbilityBase
    {
        public override int SpeedDiceNumAdder()
        {
            return 2;
        }

        public override void OnRoundStartAfter()
        {
            SetCards();
        }

        private void SetCards()
        {
            owner.allyCardDetail.ExhaustAllCards();
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 23));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 23));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 24));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 24));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 24));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 25));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 26));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 27));
            AddNewCard(new LorId(KamiyoModParameters.PackageId, 27));
        }

        private void AddNewCard(LorId id)
        {
            var card = owner.allyCardDetail.AddTempCard(id);
            card?.SetCostToZero();
        }
    }
}