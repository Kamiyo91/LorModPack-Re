using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_LuckySlice_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (!owner.passiveDetail.HasPassive<PassiveAbility_Happy_Re21341>()) return;
            owner.cardSlotDetail.RecoverPlayPointByCard(2);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                dmgRate = 25
            });
        }
    }
}