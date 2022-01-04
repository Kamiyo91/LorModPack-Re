using EmotionalBurstPassive_Re21341.Passives;

namespace EmotionalBurstPassive_Re21341.Cards
{
    public class DiceCardSelfAbility_Headbutt_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (!owner.passiveDetail.HasPassive<PassiveAbility_Angry_Re21341>()) return;
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPoint(1);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                dmg = 10,
                dmgRate = 50
            });
        }

        public override void OnSucceedAttack()
        {
            owner.TakeDamage(10);
        }
    }
}