using System.Linq;
using BLL_Re21341.Models;
using EmotionalBurstPassive_Re21341.Buffs;
using Omori_Re21341.MapManagers;
using Util_Re21341;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_OmoriMassAttackErase_Re21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;
        private bool _used;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.cardSlotDetail.cardAry.Exists(x =>
                x?.card?.GetID() == new LorId(ModParameters.PackageId, 67));
        }

        public override void OnEndAreaAttack()
        {
            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnSucceedAreaAttack(BattleUnitModel target)
        {
            target?.bufListDetail.AddReadyBuf(new BattleUnitBuf_Afraid_Re21341());
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

        public override void OnUseCard()
        {
            if (owner.emotionDetail.EmotionLevel > 1)
            {
                var dice = card.card.CreateDiceCardBehaviorList().FirstOrDefault();
                card.AddDice(dice);
                if (owner.emotionDetail.EmotionLevel > 3) card.AddDice(dice);
                if (owner.emotionDetail.EmotionLevel > 4) card.AddDice(dice);
            }

            if (owner.faction != Faction.Player ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _used = true;
            ChangeToOmoriEgoMap();
        }

        public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("Omori2_Re21341", 8);
        }

        private static void ChangeToOmoriEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Omori2_Re21341",
                StageId = 8,
                IsPlayer = true,
                OneTurnEgo = true,
                Component = new Omori2_Re21341MapManager(),
                Bgy = 0.45f
            });
        }
    }
}