using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Extensions;
using BigDLL4221.Models;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.Kamiyo_Re21341.Passives;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo1_21341 : EmotionCardAbilityBase
    {
        public override void OnRoundStart()
        {
            _owner.TakeDamage(2);
            _owner.TakeBreakDamage(2);
            SetRedirectSpeedDie();
        }

        public override void OnSelectEmotion()
        {
            ActiveEgo();
            _owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 31));
            SetRedirectSpeedDie();
        }

        public override void OnWaveStart()
        {
            ActiveEgo();
            _owner.passiveDetail.AddPassive(new LorId(KamiyoModParameters.PackageId, 31));
            SetRedirectSpeedDie();
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

        private void SetRedirectSpeedDie()
        {
            if (!ModParameters.PassiveOptions.TryGetValue(KamiyoModParameters.PackageId, out var passiveOptions))
                return;
            var passiveItem = passiveOptions.FirstOrDefault(x => x.PassiveId == 14);
            if (passiveItem == null || (passiveItem.ForceAggroOptions != null &&
                                        passiveItem.ForceAggroOptions.ForceAggroSpeedDie.Contains(
                                            _owner.speedDiceResult.Count - 2))) return;
            var index = passiveOptions.IndexOf(passiveItem);
            passiveItem.ForceAggroOptions =
                new ForceAggroOptions(forceAggroSpeedDie: new List<int> { _owner.speedDiceResult.Count - 2 });
            if (index != -1) passiveOptions[index] = passiveItem;
        }
    }
}