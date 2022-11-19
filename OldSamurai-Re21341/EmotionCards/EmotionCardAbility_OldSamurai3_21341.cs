using System.Linq;
using KamiyoModPack.BLL_Re21341.Models;
using KamiyoModPack.OldSamurai_Re21341.Passives;

namespace KamiyoModPack.OldSamurai_Re21341.EmotionCards
{
    public class EmotionCardAbility_OldSamurai3_21341 : EmotionCardAbilityBase
    {
        public override void OnSelectEmotion()
        {
            _owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 36));
        }

        public override void OnWaveStart()
        {
            _owner.personalEgoDetail.AddCard(new LorId(KamiyoModParameters.PackageId, 36));
        }

        public override void OnRoundStart_ignoreDead()
        {
            if (!_owner.IsDead()) return;
            var unit = BattleObjectManager.instance.GetAliveList(_owner.faction).FirstOrDefault(x =>
                x.passiveDetail.HasPassive<PassiveAbility_GhostSamuraiEmotion_Re21341>());
            unit?.Die();
        }
    }
}