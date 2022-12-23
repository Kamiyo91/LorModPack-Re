using KamiyoModPack.Kamiyo_Re21341.Cards;
using LOR_DiceSystem;

namespace KamiyoModPack.Kamiyo_Re21341.Dice
{
    public class DiceCardAbility_ShockAbsorb_Re21341 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            if (!(card.cardAbility is DiceCardSelfAbility_ShockAbsorb_Re21341 ability) || !ability.Active ||
                behavior.Type == BehaviourType.Standby) return;
            if (card?.target?.currentDiceAction?.cardBehaviorQueue.Count > 0)
                card?.target?.currentDiceAction?.DestroyDice(DiceMatch.AllDice);
        }
    }
}