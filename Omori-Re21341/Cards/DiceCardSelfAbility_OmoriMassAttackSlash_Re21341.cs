using BLL_Re21341.Models;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_OmoriMassAttackSlash_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;

        public override void OnEndAreaAttack()
        {
            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnSucceedAreaAttack(BattleUnitModel target)
        {
            target?.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Bleeding, 1, owner);
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChanged = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Fire);
        }

        public override void OnReleaseCard()
        {
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.cardSlotDetail.cardAry.Exists(x =>
                x != null && (x.card.GetID() == new LorId(ModParameters.PackageId, 66) ||
                              x.card.GetID() == new LorId(ModParameters.PackageId, 907)));
        }
    }
}