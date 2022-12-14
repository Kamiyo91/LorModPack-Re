using BigDLL4221.Extensions;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Passives;

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
            _owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 31));
        }

        public override void OnWaveStart()
        {
            ActiveEgo();
            _owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 31));
        }

        public void ActiveEgo()
        {
            var passive = _owner.GetActivePassive<PassiveAbility_AlterEgoPlayer_Re21341>();
            if (passive == null) return;
            if (!passive.Util.Model.EgoOptions.TryGetValue(passive.Util.Model.EgoPhase, out var egoOptions)) return;
            if (egoOptions.EgoActive) return;
            _owner.personalEgoDetail.RemoveCard(passive.Util.Model.FirstEgoFormCard);
            _owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 14));
            passive.Util.TurnEgoAbDialogOff();
            passive.Util.EgoActive();
        }
    }
}