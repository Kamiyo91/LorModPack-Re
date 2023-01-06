using System;
using BigDLL4221.Extensions;
using LOR_DiceSystem;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo2_21341 : EmotionCardAbilityBase
    {
        private readonly Random _random = new Random();

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (behavior.Detail != BehaviourDetail.Evasion) return;
            _owner.SetEmotionCombatLog(_emotionCard);
            _owner.RecoverHP(2);
        }

        public override DiceStatBonus GetDiceStatBonus(BehaviourDetail behaviour)
        {
            return behaviour == BehaviourDetail.Evasion
                ? new DiceStatBonus { power = _random.Next(1, 2) }
                : base.GetDiceStatBonus(behaviour);
        }
    }
}