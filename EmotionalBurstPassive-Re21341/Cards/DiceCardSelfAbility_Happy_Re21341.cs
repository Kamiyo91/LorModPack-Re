using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Re21341.Models;
using BLL_Re21341.Models.Enum;
using EmotionalBurstPassive_Re21341.Passives;
using Util_Re21341;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Happy_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Play]Add Emotion [Happy] in Passives([Using it more times will increase its effects]) and remove other Emotion Passives this Scene\n[Happy]:\nGain 1/2/3 [Haste] each Scene.[On Dice Roll]Boost the *maximum* Dice Roll by 1/2/3 or Lower the *maximum* Dice Roll by 1/2/3 at 10%/20%/30% chance.At the end of each Scene change all Emotions Coin Type in [Positive Coin]";

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
            EmotionalBurstUtil.RemoveEmotionalBurstCards(unit);
        }

        private static void Activate(BattleUnitModel unit)
        {
            EmotionalBurstUtil.RemoveAllEmotionalPassives(unit, EmotionBufEnum.Happy);
            if (unit.passiveDetail.PassiveList.Find(x =>
                    x is PassiveAbility_Happy_Re21341) is PassiveAbility_Happy_Re21341 passiveHappy)
            {
                var stacks = passiveHappy.GetStack();
                if (stacks >= 3) return;
                passiveHappy.ChangeNameAndSetStacks(stacks + 1);
                passiveHappy.InstantIncrease();
                return;
            }
            var passive =
                unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 19)) as
                    PassiveAbility_Happy_Re21341;
            passive?.ChangeNameAndSetStacks(1);
            passive?.AfterInit();
            unit.passiveDetail.OnCreated();
        }
    }
}
