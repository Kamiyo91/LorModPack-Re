using System.Linq;
using OldSamurai_Re21341.Passives;
using Util_Re21341.CommonPassives;

namespace OldSamurai_Re21341.Cards
{
    public class DiceCardSelfAbility_SpecialTypeAncestralCall_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= 5 ||
                    owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>()) &&
                   !owner.bufListDetail.HasAssimilation() &&
                   BattleObjectManager.instance.GetAliveList(Faction.Player).All(x => x == owner);
        }

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            var passive =
                unit.passiveDetail.PassiveList.Find(x => x is PassiveAbility_OldSamurai_Re21341) as
                    PassiveAbility_OldSamurai_Re21341;
            passive?.ForcedEgo();
        }
    }
}