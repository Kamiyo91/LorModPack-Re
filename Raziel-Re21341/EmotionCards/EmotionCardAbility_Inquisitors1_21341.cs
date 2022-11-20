using BigDLL4221.Utils;

namespace KamiyoModPack.Raziel_Re21341.EmotionCards
{
    public class EmotionCardAbility_Inquisitors1_21341 : EmotionCardAbilityBase
    {
        private bool _revive;

        public override void OnSelectEmotion()
        {
            _revive = false;
        }

        public override void OnWaveStart()
        {
            _revive = false;
        }

        public override void OnRoundStart_ignoreDead()
        {
            if (_revive || !_owner.IsDead()) return;
            _revive = true;
            UnitUtil.UnitReviveAndRecovery(_owner, _owner.MaxHp, true);
        }
    }
}