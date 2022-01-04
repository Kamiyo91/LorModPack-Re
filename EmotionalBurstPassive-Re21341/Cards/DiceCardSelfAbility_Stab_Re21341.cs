using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Stab_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (card.target != null && card.target.passiveDetail.HasPassive<PassiveAbility_Happy_Re21341>())
            {
                owner.allyCardDetail.DrawCards(1);
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    dmgRate = 50
                });
            }

            if (owner.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>())
                card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
                {
                    dmgRate = 50
                });
        }
    }
}