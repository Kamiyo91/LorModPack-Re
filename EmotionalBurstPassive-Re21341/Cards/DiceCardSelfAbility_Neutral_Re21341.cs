﻿using BLL_Re21341.Models;
using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Neutral_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(targetUnit);
            self.exhaust = true;
            EmotionalBurstUtil.RemoveEmotionalBurstCards(unit);
        }

        private static void Activate(BattleUnitModel unit)
        {
            EmotionalBurstUtil.RemoveAllEmotionalPassives(unit);
            AddNeutralPassive(unit);
        }

        private static void AddNeutralPassive(BattleUnitModel unit)
        {
            if (unit.passiveDetail.HasPassive<PassiveAbility_Neutral_Re21341>()) return;
            unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 32));
            unit.passiveDetail.OnCreated();
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}