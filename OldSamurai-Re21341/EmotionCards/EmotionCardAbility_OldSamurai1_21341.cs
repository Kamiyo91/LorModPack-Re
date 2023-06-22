using KamiyoModPack.BLL_Re21341.Models;
using LOR_DiceSystem;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.EmotionCards
{
    public class EmotionCardAbility_OldSamurai1_21341 : EmotionCardAbilityBase
    {
        public override void OnStartBattle()
        {
            _owner.ReadyCounterCard(37, KamiyoModParameters.PackageId);
        }

        public override DiceStatBonus GetDiceStatBonus(BehaviourDetail behaviour)
        {
            if (!_owner.bufListDetail.IsNullifyPower()) return base.GetDiceStatBonus(behaviour);
            _owner.SetEmotionCombatLog(_emotionCard);
            return new DiceStatBonus { min = 1, max = 1 };
        }

        public override void OnRoundStart()
        {
            if (UnitUtil.SupportCharCheck(_owner, true) == 1)
                _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, _owner);
        }
    }
}