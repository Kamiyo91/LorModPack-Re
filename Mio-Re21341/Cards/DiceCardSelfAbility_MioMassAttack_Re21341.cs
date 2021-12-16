using BLL_Re21341.Models;
using Mio_Re21341.Buffs;
using Util_Re21341;

namespace Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_MioMassAttack_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;
        private bool _used;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.HasBuf<BattleUnitBuf_GodAuraRelease_Re21341>() ||
                   owner.bufListDetail.HasBuf<BattleUnitBuf_CorruptedGodAuraRelease_Re21341>();
        }

        public override void OnEndAreaAttack()
        {
            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnStartBattle()
        {
            _used = true;
            if (owner.faction != Faction.Player ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            ChangeToMioEgoMap();
        }

        private static void ChangeToMioEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Mio_Re21341",
                StageId = 2,
                OneTurnEgo = true,
                IsPlayer = true,
                Component = new Mio_Re21341MapManager(),
                Bgy = 0.2f
            });
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("Mio_Re21341", 2);
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