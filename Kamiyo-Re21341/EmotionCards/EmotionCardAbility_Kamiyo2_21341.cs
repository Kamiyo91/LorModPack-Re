using System;
using LOR_DiceSystem;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo2_21341 : EmotionCardAbilityBase
    {
        private readonly Random _random = new Random();

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Evasion) _owner.RecoverHP(3);
        }

        public override DiceStatBonus GetDiceStatBonus(BehaviourDetail behaviour)
        {
            return behaviour == BehaviourDetail.Evasion
                ? new DiceStatBonus { power = _random.Next(1, 2) }
                : base.GetDiceStatBonus(behaviour);
        }
    }
}