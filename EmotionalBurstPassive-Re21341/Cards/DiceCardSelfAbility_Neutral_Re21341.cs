﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using EmotionalBurstPassive_Re21341.Passives;
using Util_Re21341;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Neutral_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Play]Add Emotion [Neutral] in Passives and remove other Emotion Passives this Scene\n[Neutral]:\nDraw one additional page and Restore 1 Light each Scene.";

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
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
            unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 20));
            unit.passiveDetail.OnCreated();
        }
    }
}
