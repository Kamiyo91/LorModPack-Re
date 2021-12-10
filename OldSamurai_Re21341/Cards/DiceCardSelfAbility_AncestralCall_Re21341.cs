using System.Linq;

namespace OldSamurai_Re21341.Cards
{
    public class DiceCardSelfAbility_AncestralCall_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc = "A";
        public override bool OnChooseCard(BattleUnitModel owner) => owner.emotionDetail.EmotionLevel >= 5 &&
                                                                    BattleObjectManager.instance.GetAliveList(Faction.Player).All(x => x == owner);
    }
}
