namespace Mio_Re21341.Cards
{
    public class DiceCardSelfAbility_GodAuraCard_Re21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel >= 4 &&
                   !owner.bufListDetail.HasAssimilation();
        }
    }
}