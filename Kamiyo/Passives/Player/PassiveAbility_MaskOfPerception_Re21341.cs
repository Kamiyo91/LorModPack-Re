﻿using LOR_DiceSystem;
using Util_Re21341;

namespace Kamiyo_Re21341.Passives.Player
{
    public class PassiveAbility_MaskOfPerception_Re21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Evasion) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            base.OnStartTargetedOneSide(attackerCard);
            UnitUtil.SetPassiveCombatLog(this, owner);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            base.OnStartParrying(card);
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel;
            if (card == null)
            {
                battlePlayingCardDataInUnitModel = null;
            }
            else
            {
                var target = card.target;
                battlePlayingCardDataInUnitModel = target?.currentDiceAction;
            }

            var battlePlayingCardDataInUnitModel2 = battlePlayingCardDataInUnitModel;
            UnitUtil.SetPassiveCombatLog(this, owner);
            battlePlayingCardDataInUnitModel2?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }
    }
}
