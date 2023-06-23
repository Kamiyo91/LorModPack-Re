using KamiyoModPack.OldSamurai_Re21341.Buffs;
using UtilLoader21341.Util;

namespace KamiyoModPack.OldSamurai_Re21341.EmotionCards
{
    public class EmotionCardAbility_OldSamurai2_21341 : EmotionCardAbilityBase
    {
        public override void OnParryingStart(BattlePlayingCardDataInUnitModel card)
        {
            _owner.SetEmotionCombatLog(_emotionCard);
            card?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                min = +1,
                max = +1
            });
        }

        public override void OnSelectEmotion()
        {
            _owner.bufListDetail.AddBuf(new BattleUnitBuf_SamuraiDamageUp_21341());
        }

        public override void OnWaveStart()
        {
            _owner.bufListDetail.AddBuf(new BattleUnitBuf_SamuraiDamageUp_21341());
        }
    }
}