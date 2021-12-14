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
        public static string Desc =
            "[On Play]Add Emotion [Angry] in Passives([Using it more times will increase its effects]) and remove other Emotion Passives this Scene\n[Angry]:\nGain 1/2/3 [Strength],inflict on self 1/2/3 [Disarm] and 3/6/9 [Fragile] each Scene.Each time this Character takes damage Gain 1 [Negative Emotion Coin]";

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
                unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 21)) as
                    PassiveAbility_Angry_Re21341;
            passive?.ChangeNameAndSetStacks(1);
            passive?.AfterInit();
            unit.passiveDetail.OnCreated();
        }
    }
}
