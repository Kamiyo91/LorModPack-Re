using System.Linq;
using BigDLL4221.Extensions;

namespace KamiyoModPack.Kamiyo_Re21341.EmotionCards
{
    public class EmotionCardAbility_Kamiyo3_21341 : EmotionCardAbilityBase
    {
        public override void OnRoundStart()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList().Where(x => x != _owner))
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 1, _owner);
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var target = behavior.card?.target;
            if (target == null) return;
            _owner.SetEmotionCombatLog(_emotionCard);
            target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 1, _owner);
            if (target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn) != null) _owner.breakDetail.RecoverBreak(3);
        }
    }
}