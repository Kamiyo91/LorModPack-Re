using System.Linq;
using KamiyoModPack.Hayate_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.Hayate_Re21341.EmotionCards
{
    public class EmotionCardAbility_Hayate2_21341 : EmotionCardAbilityBase
    {
        public override void OnRoundStart()
        {
            _owner.cardSlotDetail.RecoverPlayPoint(_owner.cardSlotDetail.GetMaxPlayPoint());
            _owner.allyCardDetail.DrawCards(2);
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, _owner);
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, _owner);
            foreach (var unit in BattleObjectManager.instance.GetAliveList().Where(x => x != _owner))
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 3, _owner);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            _owner.SetEmotionCombatLog(_emotionCard);
            behavior.card?.target?.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Vulnerable, 1, _owner);
        }

        public override void OnWaveStart()
        {
            if (_owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_EmotionRage_21341) == null)
                _owner.bufListDetail.AddBuf(new BattleUnitBuf_EmotionRage_21341());
        }

        public override void OnSelectEmotion()
        {
            _owner.bufListDetail.AddBuf(new BattleUnitBuf_EmotionRage_21341());
        }
    }
}