using BigDLL4221.Utils;
using KamiyoModPack.BLL_Re21341.Models;
using LOR_DiceSystem;

namespace KamiyoModPack.OldSamurai_Re21341.EmotionCards
{
    public class EmotionCardAbility_OldSamurai1_21341 : EmotionCardAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(_owner, 37, KamiyoModParameters.PackageId);
        }

        public override DiceStatBonus GetDiceStatBonus(BehaviourDetail behaviour)
        {
            if (!_owner.bufListDetail.IsNullifyPower()) return base.GetDiceStatBonus(behaviour);
            _owner.battleCardResultLog.SetEmotionAbility(true, _emotionCard, _emotionCard.XmlInfo.id);
            return new DiceStatBonus { min = 1, max = 1 };
        }

        public override void OnRoundStart()
        {
            if (UnitUtil.SupportCharCheck(_owner, true) == 1)
                _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, _owner);
        }
    }
}