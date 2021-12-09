namespace Kamiyo_Re21341.Cards
{
    internal class DiceCardSelfAbility_AlterEgoCard_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "[Single Use]\nCan be used at Emotion Level 4 or above\n[On Use] Unleash Alter Ego's Power,recover full Stagger Resist and full Light next Scene.";

        public override bool OnChooseCard(BattleUnitModel owner) => owner.emotionDetail.EmotionLevel >= 4 && !owner.bufListDetail.HasAssimilation();
    }
}
