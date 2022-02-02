namespace Hayate_Re21341.Cards
{
    public class DiceCardSelfAbility_FingersnapSpecial_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChange;

        public override void OnStartBattle()
        {
            if (_motionChange)
            {
                _motionChange = false;
                owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            }

            card.target.Die(owner);
        }

        public override bool IsTargetChangable(BattleUnitModel attacker)
        {
            return false;
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChange = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);
        }

        public override void OnReleaseCard()
        {
            _motionChange = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnRoundEnd_inHand(BattleUnitModel unit, BattleDiceCardModel self)
        {
            self.exhaust = true;
        }
    }
}