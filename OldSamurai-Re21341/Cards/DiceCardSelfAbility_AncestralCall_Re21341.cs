using System.Linq;
using Util_Re21341.CommonPassives;

namespace OldSamurai_Re21341.Cards
{
    public class DiceCardSelfAbility_AncestralCall_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return (owner.emotionDetail.EmotionLevel >= 5 ||
                    owner.passiveDetail.HasPassive<PassiveAbility_KurosawaStory_Re21341>()) &&
                   !owner.bufListDetail.HasAssimilation() &&
                   BattleObjectManager.instance.GetAliveList(Faction.Player).All(x => x == owner);
        }
    }
}