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
    public class DiceCardSelfAbility_Sad_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[On Play]Add Emotion [Sad] in Passives([Using it more times will increase its effects]) and remove other Emotion Passives this Scene\n[Sad]:\nGain 1/2/3 [Endurance] and 2/4/6 [Protection], inflict on self 1/2/3 [Bind] each Scene.At the end of each Scene change all Emotions Coin Type in [Negative Coin]";

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
            EmotionalBurstUtil.RemoveEmotionalBurstCards(unit);
        }

        private static void Activate(BattleUnitModel unit)
        {
            EmotionalBurstUtil.RemoveAllEmotionalPassives(unit, EmotionBufEnum.Sad);
            if (unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_Sad_Re21341) is
                PassiveAbility_Sad_Re21341 passiveSad)
            {
                var stacks = passiveSad.GetStack();
                if (stacks >= 3) return;
                passiveSad.ChangeNameAndSetStacks(stacks + 1);
                passiveSad.InstantIncrease();
                return;
            }

            var passive =
                unit.passiveDetail.AddPassive(new LorId(ModParameters.PackageId, 22)) as
                    PassiveAbility_Sad_Re21341;
            passive?.ChangeNameAndSetStacks(1);
            passive?.AfterInit();
            unit.passiveDetail.OnCreated();
        }
    }
}
