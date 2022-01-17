using BLL_Re21341.Models;
using Mio_Re21341.Buffs;

namespace Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_MioMassAttack_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.HasBuf<BattleUnitBuf_GodAuraRelease_Re21341>() ||
                   owner.bufListDetail.HasBuf<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>();
        }

        public override void OnEndAreaAttack()
        {
            if (!(owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_SakuraPetal_Re21341) is
                    BattleUnitBuf_SakuraPetal_Re21341 buf))
            {
                buf = new BattleUnitBuf_SakuraPetal_Re21341();
                owner.bufListDetail.AddBufWithoutDuplication(buf);
                owner.personalEgoDetail.AddCard(new LorId(ModParameters.PackageId, 59));
            }

            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChanged = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);
        }

        public override void OnReleaseCard()
        {
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}