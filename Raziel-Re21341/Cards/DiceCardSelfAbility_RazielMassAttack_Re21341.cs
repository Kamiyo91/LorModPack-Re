using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using Util_Re21341;

namespace Raziel_Re21341.Cards
{
    public class DiceCardSelfAbility_RazielMassAttack_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;
        private bool _used;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel > 3;
        }

        public override void OnEndAreaAttack()
        {
            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnStartBattle()
        {
            if (owner.faction != Faction.Player ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _used = true;
            ChangeToWiltonEgoMap();
        }

        private static void ChangeToWiltonEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Raziel_Re21341",
                StageId = 7,
                OneTurnEgo = true,
                IsPlayer = true,
                Component = new Raziel_Re21341MapManager(),
                Bgy = 0.2f
            });
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("Wilton_Re21341", 6);
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChanged = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Guard);
        }

        public override void OnReleaseCard()
        {
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}
