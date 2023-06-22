using KamiyoModPack.Mio_Re21341.Passives;
using UtilLoader21341.Util;

namespace KamiyoModPack.Mio_Re21341.EmotionCards
{
    public class EmotionCardAbility_Mio1_21341 : EmotionCardAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            behavior.card?.target?.TakeDamage(2);
            behavior.card?.target?.TakeBreakDamage(2);
        }

        public override void OnSelectEmotion()
        {
            ActiveEgo();
        }

        public override void OnWaveStart()
        {
            ActiveEgo();
        }

        public void ActiveEgo()
        {
            _owner.GetActivePassive<PassiveAbility_GodFragment_Re21341>()?.ForcedEgo();
        }
    }
}