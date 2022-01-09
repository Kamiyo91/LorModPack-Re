using EmotionalBurstPassive_Re21341.Cards;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_SomethingGuilt_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            DiceCardSelfAbility_Sad_Re21341.Activate(owner);
        }
    }
}