using BLL_Re21341.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models.Enum;
using EmotionalBurstPassive_Re21341.Passives;
using Util_Re21341;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Angry_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
            EmotionalBurstUtil.RemoveEmotionalBurstCards(unit);
        }

        private static void Activate(BattleUnitModel unit)
        {
            EmotionalBurstUtil.RemoveAllEmotionalPassives(unit, EmotionBufEnum.Angry);
            if (unit.passiveDetail.PassiveList.Find(x =>
                    x is PassiveAbility_Angry_Re21341) is PassiveAbility_Angry_Re21341 passiveAngry)
            {
                var stacks = passiveAngry.GetStack();
                if (stacks >= 3) return;
                passiveAngry.ChangeNameAndSetStacks(stacks + 1);
                passiveAngry.InstantIncrease();
                return;
            }
            var passive =
                unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 30)) as
                    PassiveAbility_Angry_Re21341;
            passive?.ChangeNameAndSetStacks(1);
            passive?.AfterInit();
            unit.passiveDetail.OnCreated();
        }
    }
}
