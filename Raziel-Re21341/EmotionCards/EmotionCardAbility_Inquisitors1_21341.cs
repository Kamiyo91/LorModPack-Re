using KamiyoModPack.Util_Re21341.CommonPassives;

namespace KamiyoModPack.Raziel_Re21341.EmotionCards
{
    public class EmotionCardAbility_Inquisitors1_21341 : EmotionCardAbilityBase
    {
        public override void OnSelectEmotion()
        {
            _owner.passiveDetail.AddPassive(new PassiveAbility_ReviveOnce_Re21341());
        }

        public override void OnWaveStart()
        {
            _owner.passiveDetail.AddPassive(new PassiveAbility_ReviveOnce_Re21341());
        }
    }
}