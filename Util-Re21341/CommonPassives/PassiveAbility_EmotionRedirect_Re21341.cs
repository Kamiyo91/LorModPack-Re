using System;
using System.Linq;
using BigDLL4221.Models;
using LOR_DiceSystem;

namespace KamiyoModPack.Util_Re21341.CommonPassives
{
    public class PassiveAbility_EmotionRedirect_Re21341 : PassiveAbilityBase
    {
        public static Predicate<DiceMatch> AllEvadeDice = x =>
            x.abiliity.behaviourInCard.Detail == BehaviourDetail.Evasion;

        public PassiveOptions PassiveOption;

        public override void OnCreated()
        {
            Hide();
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

        public override void OnRoundStartAfter()
        {
            if (!ModParameters.PassiveOptions.TryGetValue(id.packageId, out var passiveOptions)) return;
            PassiveOption = passiveOptions.FirstOrDefault(x => x.PassiveId == id.id);
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
    }
}