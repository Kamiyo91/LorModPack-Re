using BLL_Re21341.Models;
using Kamiyo_Re21341.MapManager;
using Util_Re21341;

namespace Kamiyo_Re21341.Cards
{
    public class DiceCardSelfAbility_KamiyoMassAttack_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;
        private bool _used;
        public override bool OnChooseCard(BattleUnitModel owner) => owner.emotionDetail.EmotionLevel >= 5 && owner.bufListDetail.HasAssimilation();

        public override void OnEndAreaAttack()
        {
            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChanged = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Penetrate);
        }
        public override void OnReleaseCard()
        {
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
        public override void OnStartBattle()
        {
            _used = true;
            if (owner.faction != Faction.Player ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            ChangeToKamiyoEgoMap();
        }
        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("Kamiyo2_Re21341", 3);
        }
        private static void ChangeToKamiyoEgoMap() =>
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Kamiyo2_Re21341",
                StageId = 3,
                IsPlayer = true,
                OneTurnEgo = true,
                Component = new Kamiyo2_Re21341MapManager(),
                Bgy = 0.475f,
                Fy = 0.225f
            });
    }
}
