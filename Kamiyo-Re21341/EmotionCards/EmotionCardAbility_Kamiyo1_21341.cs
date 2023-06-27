using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Passives;
using LOR_DiceSystem;
using UtilLoader21341.Extensions;
using UtilLoader21341.Util;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo1_21341 : EmotionCardAbilityBase
    {
        public override void OnRoundStart_after()
        {
            _owner.TakeDamage(3);
            _owner.TakeBreakDamage(3);
        }

        public override void OnSelectEmotion()
        {
            ActiveEgo();
            var passive = new PassiveAbility_RedirectDiePassive_DLL21341();
            passive.SetDetailType(BehaviourDetail.Evasion);
            _owner.passiveDetail.AddPassive(passive);
        }

        public override void OnWaveStart()
        {
            ActiveEgo();
            var passive = new PassiveAbility_RedirectDiePassive_DLL21341();
            passive.SetDetailType(BehaviourDetail.Evasion);
            _owner.passiveDetail.AddPassive(passive);
        }

        public void ActiveEgo()
        {
            var passive = _owner.GetActivePassive<PassiveAbility_AlterEgoPlayer_Re21341>();
            if (passive == null) return;
            passive.EgoActived();
            if (!_owner.passiveDetail.HasPassive<PassiveAbility_MaskOfPerception_Re21341>())
                _owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 14));
        }
    }
}