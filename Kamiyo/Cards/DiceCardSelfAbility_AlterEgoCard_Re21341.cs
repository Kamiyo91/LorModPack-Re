namespace Kamiyo_Re21341.Cards
{
    internal class DiceCardSelfAbility_AlterEgoCard_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc = "A";
        public override bool OnChooseCard(BattleUnitModel owner) => owner.emotionDetail.EmotionLevel >= 4 && !owner.bufListDetail.HasAssimilation();
    }
}
