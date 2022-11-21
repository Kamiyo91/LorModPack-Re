using LOR_DiceSystem;

namespace KamiyoModPack.Mio_Re21341.EmotionCards
{
    public class EmotionCardAbility_Mio3_21341 : EmotionCardAbilityBase
    {
        private int _killCount;

        public override DiceStatBonus GetDiceStatBonus(BehaviourDetail behaviour)
        {
            return behaviour == BehaviourDetail.Slash
                ? new DiceStatBonus { power = 1 }
                : base.GetDiceStatBonus(behaviour);
        }

        public override void OnKill(BattleUnitModel target)
        {
            if (_killCount >= 3) return;
            _owner.battleCardResultLog.SetEmotionAbility(true, _emotionCard, _emotionCard.XmlInfo.id);
            _killCount++;
        }

        public override void OnSelectEmotion()
        {
            _killCount = 0;
        }

        public override void OnWaveStart()
        {
            _killCount = 0;
        }

        public override void OnRoundStart()
        {
            if (_killCount < 1) return;
            _owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, _killCount, _owner);
            _owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, _killCount, _owner);
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash) _owner.TakeBreakDamage(3);
        }
    }
}