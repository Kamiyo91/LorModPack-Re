using System;
using System.Collections.Generic;
using System.Linq;
using KamiyoModPack.BLL_Re21341.Models;
using LOR_DiceSystem;
using UtilLoader21341;
using UtilLoader21341.Models;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_EmotionRedirect_Re21341 : PassiveAbilityBase
    {
        public static Predicate<DiceMatch> AllEvadeDice = x =>
            x.abiliity.behaviourInCard.Detail == BehaviourDetail.Evasion;

        public PassiveOptionRoot PassiveOption;

        public override void OnCreated()
        {
            Hide();
        }

        public override void OnAfterRollSpeedDice()
        {
            SetRedirectSpeedDie();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (PassiveOption?.ForceAggroOptions == null) return;
            if (PassiveOption.ForceAggroOptions.ForceAggroSpeedDie.Contains(curCard.slotOrder))
                curCard.ApplyDiceStatBonus(AllEvadeDice, new DiceStatBonus
                {
                    power = 1
                });
        }

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            if (PassiveOption?.ForceAggroOptions == null) return;
            if (!PassiveOption.ForceAggroOptions.ForceAggroSpeedDie.Contains(attackerCard.targetSlotOrder)) return;
            attackerCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1,
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            if (PassiveOption?.ForceAggroOptions == null) return;
            if (!PassiveOption.ForceAggroOptions.ForceAggroSpeedDie.Contains(card.slotOrder)) return;
            var target = card.target;
            var battlePlayingCardDataInUnitModel = target?.currentDiceAction;
            battlePlayingCardDataInUnitModel?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = -1,
                max = -1
            });
        }

        private void SetRedirectSpeedDie()
        {
            var passiveItem = ModParameters.PassiveOptions.FirstOrDefault(x =>
                x.PackageId == KamiyoModParameters.PackageId && x.PassiveId == 31);
            if (passiveItem == null || (passiveItem.ForceAggroOptions != null &&
                                        passiveItem.ForceAggroOptions.ForceAggroSpeedDie.Contains(
                                            owner.speedDiceResult.Count - 2 < 0
                                                ? 0
                                                : owner.speedDiceResult.Count - 2))) return;
            var index = ModParameters.PassiveOptions.IndexOf(passiveItem);
            passiveItem.ForceAggroOptions =
                new ForceAggroOptionsRoot
                {
                    ForceAggroSpeedDie = new List<int>
                        { owner.speedDiceResult.Count - 2 < 0 ? 0 : owner.speedDiceResult.Count - 2 }
                };
            if (index != -1) ModParameters.PassiveOptions[index] = passiveItem;
            PassiveOption = passiveItem;
        }
    }
}