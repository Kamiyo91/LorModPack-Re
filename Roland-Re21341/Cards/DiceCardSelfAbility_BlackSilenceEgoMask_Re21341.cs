namespace Roland_Re21341.Cards
{
    public class DiceCardSelfAbility_BlackSilenceEgoMask_Re21341 : DiceCardSelfAbilityBase
    {
        public static string Desc =
            "Can be used at Emotion Level 4 or above\n[On Use] Unleash the power of the Black Silence's Mask next Scene";

        public override bool OnChooseCard(BattleUnitModel owner) => owner.emotionDetail.EmotionLevel >= 4 && !owner.bufListDetail.HasAssimilation();
    }
}
