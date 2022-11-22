using BigDLL4221.Extensions;
using KamiyoModPack.Mio_Re21341.Passives;

namespace KamiyoModPack.Mio_Re21341.EmotionCards
{
    public class EmotionCardAbility_Mio1_21341 : EmotionCardAbilityBase
    {
        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            _owner.SetEmotionCombatLog(_emotionCard);
            behavior.card?.target?.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 1, _owner);
        }

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
            var passive = _owner.GetActivePassive<PassiveAbility_GodFragment_Re21341>();
            if (passive == null) return;
            if (!passive.Util.Model.EgoOptions.TryGetValue(passive.Util.Model.EgoPhase, out var egoOptions)) return;
            if (egoOptions.EgoActive) return;
            _owner.personalEgoDetail.RemoveCard(passive.Util.Model.FirstEgoFormCard);
            passive.Util.TurnEgoAbDialogOff();
            passive.Util.EgoActive();
        }
    }
}