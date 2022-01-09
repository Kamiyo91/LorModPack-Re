using EmotionalBurstPassive_Re21341.Cards;
using EmotionalBurstPassive_Re21341.Passives;

namespace Omori_Re21341.Cards
{
    public class DiceCardSelfAbility_BrokenHope_Re21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (!owner.passiveDetail.HasPassive<PassiveAbility_Sad_Re21341>()) return;
            DiceCardSelfAbility_Sad_Re21341.Activate(card.target);
        }
    }
}