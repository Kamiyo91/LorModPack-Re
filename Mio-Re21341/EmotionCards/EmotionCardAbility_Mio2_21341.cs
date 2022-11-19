using System;

namespace KamiyoModPack.Mio_Re21341.EmotionCards
{
    public class EmotionCardAbility_Mio2_21341 : EmotionCardAbilityBase
    {
        private readonly Random _random = new Random();
        private int _attackCount;

        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            return _random.Next(2, 4);
        }

        public override int GetBreakDamageReduction(BattleDiceBehavior behavior)
        {
            return _random.Next(2, 4);
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            _attackCount++;
            if (_attackCount < 10) return;
            _attackCount = 0;
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
            _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Weak, 1, _owner);
            _owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Disarm, 1, _owner);
        }

        public override void OnSelectEmotion()
        {
            _attackCount = 0;
        }

        public override void OnWaveStart()
        {
            _attackCount = 0;
        }
    }
}